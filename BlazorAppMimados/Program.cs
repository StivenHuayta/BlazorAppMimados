using Mimados_WebApp.Components;
using Mimados_WebApp.Security;
using Mimados_WebApp.Services;

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

// Servicios de la aplicación
builder.Services.AddSingleton<VeterinariaMockDataService>();

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
