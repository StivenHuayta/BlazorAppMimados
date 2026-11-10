using BlazorAppMimados.Models;
using Mimados_WebApp.Components.Pages.Cliente;
using Mimados_WebApp.Models;
using Mimados_WebApp.Services.MascotaServices;
using Mimados_WebApp.Services.SedeService;
using System.Text.Json;

namespace Mimados_WebApp.Services.SedeService
{
    public class SedeServiceImpl: ISedeService
{
        private readonly HttpClient _http;

        public SedeServiceImpl(HttpClient http)
        {
            _http = http;
        }

        /*
        private const string BASE_URL = "http://localhost:8080/MimadosRS/webresources/SedeRS";
    private readonly HttpClient _httpClient;//comantario
    private readonly ILogger<SedeServiceImpl> _logger;
    public SedeServiceImpl(HttpClient httpClient, ILogger<SedeServiceImpl> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }
    */

        public async Task<List<Sede>> ObtenerSedesAsync()
        {
            var respuesta = await _http.GetFromJsonAsync<ApiResponse<List<Sede>>>(
                $"SedeRS/listarSede");

            return respuesta?.Data ?? new List<Sede>();
        }



        public async Task<bool> CrearSedeAsync(Sede sede)
        {
            var datos = new
            {
                nombre = sede.Nombre,
                direccion = sede.Direccion,
                telefono = sede.Telefono
            };


            var response = await _http.PostAsJsonAsync("SedeRS/registrar", datos);

            if (!response.IsSuccessStatusCode)
                return false;

            var resultado = await response.Content
                .ReadFromJsonAsync<ApiResponse<object>>();

            return resultado?.Success == true;
        }


        public class SedeJavaResponse
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public bool Activo { get; set; }

    }
        

}
}
