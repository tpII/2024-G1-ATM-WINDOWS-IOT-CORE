using ATM.Application.Interfaces;
using ATM.Application.Interfaces.Repositories;
using ATM.Application.Interfaces.Services;
using ATM.Application.Interfaces.Security;
using ATM.Application.Services;
using ATM.Application.UseCases;
using ATM.Infrastructure.Services;
using ATM.Infrastructure.Hubs;
using ATM.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:5000"); // Escuchar en todas las interfaces

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IRfidReader, Windows10RfidReader>();
builder.Services.AddHostedService<RfidBackgroundService>();

// Configurar repositorios
builder.Services.AddHttpClient<ICardRepository, CardRepository>(client =>
{
    client.BaseAddress = new Uri("http://192.168.0.148:5000"); // IP del servidor con la DB
});

// Configurar servicios
builder.Services.AddTransient<IEncryptionService, EncryptionService>();
builder.Services.AddSingleton<SessionService>();

// Registrar casos de uso
builder.Services.AddTransient<EnterCardUseCase>();
builder.Services.AddTransient<EnterPinUseCase>();

builder.Services.AddControllers();

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

app.MapHub<CardNotificationHub>("/cardNotificationHub");

app.MapControllers();

app.Run();
