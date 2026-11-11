namespace Mimados.WebApp.Services;
using Mimados.WebApp.Models;


    public interface IMascotaService
{

    Task<Mascota?> ObtenerMascotaPorIdAsync(int mascotaId);
    Task<List<Mascota>> ObtenerMascotasPorClienteAsync(int clienteId);
    Task<bool> CrearMascotaAsync(Mascota mascota);
    Task<bool> ActualizarMascotaAsync(Mascota mascota);
    Task<bool> EliminarMascotaAsync(int mascotaId);
    Task<List<RegistroMedico>> ObtenerHistorialAsync(int mascotaId);
}

