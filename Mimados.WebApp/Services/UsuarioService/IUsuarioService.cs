using Mimados.WebApp.Models;

namespace Mimados.WebApp.Services;

public interface IUsuarioService
{
    Task<UsuarioPerfilModel?> ObtenerPerfilAsync(int idUsuario);
    Task<bool> ActualizarPerfilAsync(UsuarioPerfilModel perfil);
    Task<bool> EliminarCuentaLogicaAsync(int idUsuario);
}