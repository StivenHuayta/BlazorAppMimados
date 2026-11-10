using Mimados_WebApp.Services.MascotaServices;
using BlazorAppMimados.Services;
using Mimados_WebApp.Components;
using Mimados_WebApp.Security;
using Mimados_WebApp.Services;
using Mimados_WebApp.Services.CitaServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCookieAuth();
builder.Services.AddHttpClient<IAuthValidationService, AuthValidationService>();

// URL Base del Backend Java REST
string apiBaseUrl = builder.Configuration["ApiBaseUrl"] ?? "http://localhost:8080/mimados-rs/rs/";

// Registro de Servicios REST HTTP para Empleados y Horarios
builder.Services.AddHttpClient<IEmpleadoService, EmpleadoService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<IHorarioLaboralService, HorarioLaboralService>(client =>
{
    client.BaseAddress = new Uri(apiBaseUrl);
});

builder.Services.AddHttpClient<ICitaService, CitaService>(client =>
{
    client.BaseAddress = new Uri(
        "http://localhost:8080/MimadosRS/webresources/");
});

builder.Services.AddHttpClient<IDisponibilidadService, DisponibilidadService>(client =>
{
    client.BaseAddress = new Uri(
        "http://localhost:8080/MimadosRS/webresources/");
});

builder.Services.AddHttpClient<IVeterinarioService, VeterinarioService>(client =>
{
    client.BaseAddress = new Uri(
        "http://localhost:8080/MimadosRS/webresources/");
});

// Servicios de la aplicación
builder.Services.AddSingleton<VeterinariaMockDataService>();

//PARA SERVICIO DE MASCOTA
builder.Services.AddScoped<HttpClient>();
builder.Services.AddScoped<IMascotaService, MascotaService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

// Middlewares de Autenticación y Autorización
app.UseAuthentication();
app.UseAuthorization();

// Endpoints de autenticación (/auth/login y /auth/logout)
app.MapAuthEndpoints();

app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
