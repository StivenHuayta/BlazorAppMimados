using BlazorAppMimados.Models;
using Mimados_WebApp.Models;

namespace Mimados_WebApp.Services.ServicioService
{
    public class ServicioServiceImpl: IServicioService
{
        private readonly HttpClient _http;

        public ServicioServiceImpl(HttpClient http)
        {
            _http = http;
        }

        public async Task<Servicio?> ObtenerServicioPorIdAsync(int id)
        {
            var respuesta = await _http.GetFromJsonAsync<ApiResponse<Servicio>>(
            $"ServicioRS/{id}");

            return respuesta?.Data;
        }
        public async Task<List<Servicio>> ObtenerServiciosAsync()
        {
            var respuesta = await _http.GetFromJsonAsync<ApiResponse<List<Servicio>>>(
                $"ServicioRS/listarServicios");

            return respuesta?.Data ?? new List<Servicio>(); 

        }
        public async Task<bool> CrearServicioAsync(Servicio servicio)
        {
            var datos = new
            {
                nombre = servicio.Nombre,
                descripcion = servicio.Descripcion,
                precioReferencial = servicio.PrecioReferencial,
                duracionMinutos = servicio.DuracionMinutos

            };


            var response = await _http.PostAsJsonAsync("ServicioRS/registrar", datos);

            if (!response.IsSuccessStatusCode)
                return false;

            var resultado = await response.Content
                .ReadFromJsonAsync<ApiResponse<object>>();

            return resultado?.Success == true;
        }



        public class ServicioJavaResponse
        {
            public int Id { get; set; }
            public string? Nombre { get; set; }
            public string? Direccion { get; set; }
            public string? Telefono { get; set; }
            public bool Activo { get; set; }



        }
    }
}
