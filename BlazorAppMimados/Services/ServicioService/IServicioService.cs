using Mimados_WebApp.Models;

namespace Mimados_WebApp.Services.ServicioService
{
    public interface IServicioService
{
        Task<Servicio?> ObtenerServicioPorIdAsync(int id);
        Task<List<Servicio>> ObtenerServiciosAsync();
        Task<bool> CrearServicioAsync(Servicio servicio);
        //Task<bool> ActualizarServicioAsync(Servicio servicio);
        //Task<bool> EliminarServicioAsync(int id);
    }
}
