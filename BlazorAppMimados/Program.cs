using Mimados_WebApp.Services.MascotaServices;
using Mimados_WebApp.Components;
using Mimados_WebApp.Security;
using Mimados_WebApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Autenticación por Cookies (similar al ejemplo de referencia en SoftProg-WebApp)
builder.Services.AddCookieAuth();
builder.Services.AddHttpClient<IAuthValidationService, AuthValidationService>();

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
