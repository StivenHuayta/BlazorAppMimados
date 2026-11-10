using BlazorAppMimados.Models;
using Mimados_WebApp.Components.Pages.Cliente;
using Mimados_WebApp.Models;
using Mimados_WebApp.Models.DTO;
using System.Buffers.Text;

namespace Mimados_WebApp.Services.SedeServicioService
{
    public class SedeServicioService:ISedeServicioService
{
        private readonly HttpClient _http;

        public SedeServicioService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<SedeServicioDTO>> ObtenerServiciosPorSedeAsync(int sedeid)
        {
            var respuesta = await _http.GetFromJsonAsync<ApiResponse<List<SedeServicioDTO>>>(
            $"SedeServicioRS/listarSedeServicios/{sedeid}");

            return respuesta?.Data;
        }

        /*
        public async Task<List<Servicio>> ObtenerServiciosDisponiblesAsync(int sedeid)
        {
            var respuesta = await _http.GetFromJsonAsync<ApiResponse<List<SedeServicioDTO>>>(
            $"SedeServicioRS/listarSedeServicios/{sedeid}");

            return respuesta?.Data;
        }
        */

        public async Task<bool> EliminarSedeServicioAsync(int sedeServicioId)
        {
           
            
                var response = await _http.DeleteAsync($"SedeServicioRS/{sedeServicioId}");
                return response.IsSuccessStatusCode;
            
        }
    }
}
