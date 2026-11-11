using Mimados.WebApp.Models;
using Mimados.WebApp.Models.DTO;

namespace Mimados.WebApp.Services.SedeServicioService
{
    public interface ISedeServicioService
{
        Task<List<SedeServicioDTO>> ObtenerServiciosPorSedeAsync(int sedeid);

        Task<List<Servicio>> ObtenerServiciosDisponiblesAsync(int sedeid);

        Task<bool> CrearSedeServicioAsync(SedeServicio sedeServicio);



        Task<bool> EliminarSedeServicioAsync(int sedeServicioId);
    }
}
