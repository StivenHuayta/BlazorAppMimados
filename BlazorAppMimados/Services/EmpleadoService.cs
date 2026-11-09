using System.Net.Http.Json;
using BlazorAppMimados.Models;

namespace BlazorAppMimados.Services;

public interface IEmpleadoService
{
    Task<ApiResponse<object>?> RegistrarEmpleadoAsync(EmpleadoRequest request);
    Task<ApiResponse<object>?> ModificarEmpleadoAsync(EmpleadoRequest request);
    Task<ApiResponse<List<EmpleadoRequest>>?> ListarEmpleadosAsync();
}

public class EmpleadoService : IEmpleadoService
{
    private readonly HttpClient _http;

    public EmpleadoService(HttpClient http)
    {
        _http = http;
    }

    public async Task<ApiResponse<object>?> RegistrarEmpleadoAsync(EmpleadoRequest request)
    {
        var response = await _http.PostAsJsonAsync("EmpleadoRS/registrar", request);
        return await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
    }

    public async Task<ApiResponse<object>?> ModificarEmpleadoAsync(EmpleadoRequest request)
    {
        var response = await _http.PostAsJsonAsync("EmpleadoRS/modificar", request);
        return await response.Content.ReadFromJsonAsync<ApiResponse<object>>();
    }

    public async Task<ApiResponse<List<EmpleadoRequest>>?> ListarEmpleadosAsync()
    {
        return await _http.GetFromJsonAsync<ApiResponse<List<EmpleadoRequest>>>("EmpleadoRS/listar");
    }
}
