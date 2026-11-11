using System.Net.Http.Json;
using Mimados.WebApp.Models;

namespace Mimados.WebApp.Services;

public interface IHorarioLaboralService
{
    Task<ApiResponse<object>?> RegistrarHorarioAsync(HorarioRequest request);
    Task<ApiResponse<List<HorarioRequest>>?> ListarPorEmpleadoAsync(int idEmpleado);
    Task<ApiResponse<object>?> EliminarHorarioAsync(int idHorario);
}

public class HorarioLaboralService : IHorarioLaboralService
{
    private readonly HttpClient _http;

    public HorarioLaboralService(HttpClient http)
    {
        _http = http;
    }

    public async Task<ApiResponse<object>?> RegistrarHorarioAsync(HorarioRequest request)
    {
        var response = await _http.PostAsJsonAsync("HorarioLaboralRS/registrar", request);
        return await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
    }

    public async Task<ApiResponse<List<HorarioRequest>>?> ListarPorEmpleadoAsync(int idEmpleado)
    {
        return await _http.GetFromJsonAsync<ApiResponse<List<HorarioRequest>>>($"HorarioLaboralRS/listarPorEmpleado/{idEmpleado}");
    }

    public async Task<ApiResponse<object>?> EliminarHorarioAsync(int idHorario)
    {
        var request = new HorarioRequest { Id = idHorario };
        var response = await _http.PostAsJsonAsync("HorarioLaboralRS/eliminar", request);
        return await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
    }
}
