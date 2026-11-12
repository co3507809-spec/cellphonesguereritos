using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using cellphonesguereritos.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient(); // Para llamar a la API de Gemini desde el ChatController

// Registrar el contexto de la base de datos de Entity Framework
builder.Services.AddDbContext<CellphonesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CellphonesConnection")));
// Configuración de Seguridad
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Login"; // Si no están logueados, los manda aquí
        options.AccessDeniedPath = "/Home/Index"; // Si un cliente intenta entrar al Inventario, lo saca
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication(); // 1. Primero verifica quién eres
app.UseAuthorization();  // 2. Luego verifica qué permisos tienes

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();