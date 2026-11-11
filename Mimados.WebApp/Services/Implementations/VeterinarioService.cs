
using System.Net.Http.Json;
using Mimados.WebApp.Models;

namespace Mimados.WebApp.Services;

public class VeterinarioService : IVeterinarioService
{
    private readonly HttpClient _http;

    public VeterinarioService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<VeterinarioResumenDto>> ListarAsync()
    {
        var response = await _http.GetAsync(
            "VeterinarioRS/listar");

        var resultado = await response.Content
            .ReadFromJsonAsync<ApiResponse<List<VeterinarioResumenDto>>>();

        if (!response.IsSuccessStatusCode ||
            resultado == null ||
            !resultado.Success)
        {
            throw new Exception(
                resultado?.Message ?? "Error al listar veterinarios");
        }

        return resultado.Data ?? new List<VeterinarioResumenDto>();
    }
}
