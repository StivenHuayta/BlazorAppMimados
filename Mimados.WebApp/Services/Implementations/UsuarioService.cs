using System.Net.Http.Json;
using Mimados.WebApp.Models;

namespace Mimados.WebApp.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly HttpClient _httpClient;

        private const string PATH = "PerfilRS";

        public UsuarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UsuarioPerfilModel?> ObtenerPerfilAsync(int idUsuario)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<UsuarioPerfilModel>($"{PATH}/{idUsuario}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el perfil: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> ActualizarPerfilAsync(UsuarioPerfilModel perfil)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{PATH}/actualizar", perfil);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar: {ex.Message}");
                return false;
            }
        }


        public async Task<bool> EliminarCuentaLogicaAsync(int idUsuario)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{PATH}/eliminar/{idUsuario}");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar la cuenta: {ex.Message}");
                return false;
            }
        }



    }
}