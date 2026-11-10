using System.Net.Http.Json;
using Mimados_WebApp.Models;

namespace Mimados_WebApp.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly HttpClient _httpClient;

        // Reutilizamos el Context Root que funciono en Postman
        private const string BASE_URL = "http://localhost:8080/MimadosRS/webresources/PerfilRS";

        public UsuarioService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UsuarioPerfilModel?> ObtenerPerfilAsync(int idUsuario)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<UsuarioPerfilModel>($"{BASE_URL}/{idUsuario}");
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
                var response = await _httpClient.PutAsJsonAsync($"{BASE_URL}/actualizar", perfil);
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
                // Hace la llamada HTTP DELETE a: http://localhost:8080/MimadosRS/webresources/PerfilRS/eliminar/{id}
                var response = await _httpClient.DeleteAsync($"{BASE_URL}/eliminar/{idUsuario}");

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