namespace Mimados.WebApp.Services;

public interface IDisponibilidadService
{
    Task<List<string>> ConsultarAsync(
        int empleadoId,
        DateTime fecha,
        int duracion);
}
