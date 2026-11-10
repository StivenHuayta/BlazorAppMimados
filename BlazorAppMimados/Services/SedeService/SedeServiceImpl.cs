using Mimados_WebApp.Models;
using Mimados_WebApp.Services.MascotaServices;
using Mimados_WebApp.Services.SedeService;
using System.Text.Json;

namespace Mimados_WebApp.Services.SedeService
{
    public class SedeServiceImpl: ISedeService
{
    private const string BASE_URL = "http://localhost:8080/MimadosRS/webresources/SedeRS";
    private readonly HttpClient _httpClient;//comantario
    private readonly ILogger<SedeServiceImpl> _logger;

    public SedeServiceImpl(HttpClient httpClient, ILogger<SedeServiceImpl> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

        public async Task<List<Sede>> ObtenerSedesAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BASE_URL}/listarSede");
            if (!response.IsSuccessStatusCode) return new List<Sede>();

            var jsonString = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            // Deserializamos usando una clase DTO interna para atrapar el JSON anidado
            var responseDto = JsonSerializer.Deserialize<SedeApiResponse>(jsonString, options);
            var dataJava = responseDto?.Data;

                return dataJava?.Select(d => new Sede
            {
                Id = d.Id,
                Nombre = d.Nombre ?? "",
                Direccion = d.Direccion ?? "",
                Telefono = d.Telefono ?? "999999999",
                Activo = d.Activo,
            }).ToList() ?? new List<Sede>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al obtener las sedes ");
            return new List<Sede>();
        }




    }


    public class SedeJavaResponse
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public bool Activo { get; set; }

    }
        public class SedeApiResponse
        {
            public List<SedeJavaResponse> Data { get; set; } = new();
            public string Message { get; set; } = string.Empty;
            public bool Success { get; set; }
        }

    }
}
