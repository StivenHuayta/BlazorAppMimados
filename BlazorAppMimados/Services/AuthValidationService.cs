using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Mimados_WebApp.Models;

namespace Mimados_WebApp.Services;

/// <summary>
/// Servicio de autenticación que consume directamente el servicio REST del backend Java (mimados-rs).
/// Implementado según el documento de referencia "Creación de Servicios Web RESTFul con Jakarta REST" (Sección 9),
/// definiendo la URL base directamente mediante una constante sin requerir appsettings.json.
/// </summary>
public class AuthValidationService : IAuthValidationService
{
    // URL base según la sección 9.2 y 9.3 de la guía de Jakarta REST
    // http://localhost:8080/[ContextPath]/[PrefijoRutaDeLosServicios]/[NombreServicio]
    private const string BASE_URL = "http://localhost:8080/MimadosRS/webresources/UsuarioRS";
    private const string FALLBACK_URL = "http://localhost:8080/mimados-rs-1.0-SNAPSHOT/webresources/UsuarioRS";

    private readonly HttpClient _httpClient;
    private readonly ILogger<AuthValidationService> _logger;

    public AuthValidationService(
        HttpClient httpClient,
        ILogger<AuthValidationService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<AuthValidationResult> ValidarCredencialesAsync(string usuarioOCorreo, string contrasena)
    {
        if (string.IsNullOrWhiteSpace(usuarioOCorreo) || string.IsNullOrWhiteSpace(contrasena))
        {
            return new AuthValidationResult
            {
                Exito = false,
                MensajeError = "Por favor ingrese su usuario o correo y contraseña."
            };
        }

        var input = usuarioOCorreo.Trim();
        var loginReq = new LoginRequest(input, contrasena);

        // Rutas del servicio REST de Java a invocar (primaria y alternativa si el context path en GlassFish varía)
        var endpoints = new[]
        {
            $"{BASE_URL}/autenticar",
            $"{FALLBACK_URL}/autenticar",
            $"{BASE_URL}/login"
        };

        HttpResponseMessage? response = null;
        string? lastError = null;

        foreach (var url in endpoints)
        {
            try
            {
                using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

                var jsonContent = new StringContent(
                    JsonSerializer.Serialize(loginReq),
                    Encoding.UTF8,
                    "application/json");

                var res = await _httpClient.PostAsync(url, jsonContent, cts.Token);

                // Si no es 404 (Not Found), el recurso existe en esa URL
                if (res.StatusCode != System.Net.HttpStatusCode.NotFound)
                {
                    response = res;
                    break;
                }
            }
            catch (Exception ex)
            {
                lastError = ex.Message;
                _logger.LogDebug(ex, "Intento de conexión a {Url} no exitoso", url);
            }
        }

        if (response == null)
        {
            return new AuthValidationResult
            {
                Exito = false,
                MensajeError = $"No se pudo conectar con el servicio Java REST en {BASE_URL}. " +
                               $"Asegúrese de que el backend Java esté desplegado y corriendo en GlassFish (puerto 8080). " +
                               $"(Detalle: {lastError ?? "Servicio no disponible"})"
            };
        }

        try
        {
            var contentString = await response.Content.ReadAsStringAsync();

            // Si el servidor devolvió HTML (por ejemplo página de error 500 de GlassFish o Tomcat)
            if (string.IsNullOrWhiteSpace(contentString) || contentString.TrimStart().StartsWith("<"))
            {
                _logger.LogError("El backend Java REST devolvió HTML en lugar de JSON (Código {StatusCode}): {Body}",
                    (int)response.StatusCode, contentString);

                string mensajeHtml = response.StatusCode switch
                {
                    System.Net.HttpStatusCode.InternalServerError =>
                        "Error interno 500 en el servidor Java REST. Verifique que 'db.properties' esté configurado con la base de datos y que GlassFish tenga acceso a MySQL.",
                    System.Net.HttpStatusCode.NotFound =>
                        "El servicio REST no fue encontrado en GlassFish (Código 404). Asegúrese de desplegar mimados-rs como MimadosRS.",
                    _ => $"El servidor backend Java devolvió una respuesta no válida (Código {(int)response.StatusCode})."
                };

                return new AuthValidationResult
                {
                    Exito = false,
                    MensajeError = mensajeHtml
                };
            }

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var apiResult = JsonSerializer.Deserialize<UsuarioApiResponse>(contentString, options);

            if (response.IsSuccessStatusCode && apiResult != null && apiResult.Success && apiResult.Data != null)
            {
                var data = apiResult.Data;
                var rol = string.IsNullOrWhiteSpace(data.Rol) ? "Cliente" : data.Rol;

                return new AuthValidationResult
                {
                    Exito = true,
                    UsuarioId = data.UsuarioId,
                    Dni = data.Dni ?? string.Empty,
                    Nombres = data.Nombres ?? string.Empty,
                    Apellidos = data.Apellidos ?? string.Empty,
                    Email = string.IsNullOrWhiteSpace(data.Email) ? input : data.Email,
                    Rol = rol,
                    EsAdmin = data.EsAdmin,
                    Activo = data.Activo,
                    CodigoCmvp = data.CodigoCmvp,
                    SedeId = data.SedeId
                };
            }

            // Si el backend Java devolvió mensaje de error de negocio (401, 400, o success = false)
            var mensaje = apiResult?.Message;
            if (string.IsNullOrWhiteSpace(mensaje))
            {
                mensaje = response.StatusCode switch
                {
                    System.Net.HttpStatusCode.Unauthorized => "Credenciales incorrectas o usuario inactivo.",
                    System.Net.HttpStatusCode.BadRequest => "Datos de inicio de sesión incompletos.",
                    _ => $"Error del servidor REST ({ (int)response.StatusCode })."
                };
            }

            return new AuthValidationResult
            {
                Exito = false,
                MensajeError = mensaje
            };
        }
        catch (JsonException jEx)
        {
            _logger.LogError(jEx, "Error al deserializar JSON del servicio REST de Java");
            return new AuthValidationResult
            {
                Exito = false,
                MensajeError = "El servidor Java REST no devolvió un formato JSON válido."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error inesperado al procesar respuesta del servicio REST de Java");
            return new AuthValidationResult
            {
                Exito = false,
                MensajeError = "Error al procesar la respuesta del servicio de autenticación."
            };
        }
    }
}
