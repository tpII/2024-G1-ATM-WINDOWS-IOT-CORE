using ATM.Infrastructure.Repositories;
using ATM.Application.Interfaces.Repositories;
using ATM.Application.UseCases;
using ATM.Application.Services;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://0.0.0.0:5000"); // Escuchar en todas las interfaces


// Registrar el caso de uso
builder.Services.AddScoped<CheckBalance>();

// Registrar el servicio CheckBalanceService
builder.Services.AddScoped<CheckBalanceService>();

// Configurar el repositorio con `HttpClient`
builder.Services.AddHttpClient<IAccountRepository, AccountRepository>(client =>
{
    client.BaseAddress = new Uri("http://192.168.0.148:5000"); // IP del servidor en la PC
});


// Add services to the container.
builder.Services.AddRazorPages();

//Add controllers
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
