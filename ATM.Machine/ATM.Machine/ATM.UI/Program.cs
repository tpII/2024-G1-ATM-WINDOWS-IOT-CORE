using ATM.Application.Interfaces;
using ATM.Application.Interfaces.Repositories;
using ATM.Application.Interfaces.Services;
using ATM.Application.Interfaces.Security;
using ATM.Application.Services;
using ATM.Application.UseCases;
using ATM.Infrastructure.Services;
using ATM.Infrastructure.Hubs;
using ATM.Infrastructure.Repositories;
using System.Text.Json;


var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:5000"); // Escuchar en todas las interfaces

var uri = new Uri("http://localhost:5010");

// Configurar el repositorio de tarjetas con HttpClient
builder.Services.AddHttpClient<ICardRepository, CardRepository>(client =>
{
    client.BaseAddress = uri; // IP del servidor Node.js
});

// Configurar el repositorio de cuentas con HttpClient
builder.Services.AddHttpClient<IAccountRepository, AccountRepository>(client =>
{
    client.BaseAddress = uri; // IP del servidor Node.js
});

// Configurar el repositorio de transacciones con HttpClient
builder.Services.AddHttpClient<ITransactionRepository, TransactionRepository>(client =>
{
    client.BaseAddress = uri; // IP del servidor Node.js
});

// Configurar el repositorio de configuración de límites del cajero ATM
builder.Services.AddHttpClient<IATMConfigurationRepository, ATMConfigurationRepository>(client =>
{
    client.BaseAddress = uri; // IP del servidor Node.js
});

//Configurar el repositorio de control de dinero en el cajero ATM 
builder.Services.AddHttpClient<ICashManagementRepository, CashManagementRepository>(client =>
{
    client.BaseAddress = uri; // IP del servidor Node.js
});

// Registrar servicios
builder.Services.AddTransient<IEncryptionService, EncryptionService>();
builder.Services.AddSingleton<SessionService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IATMConfigurationService, ATMConfigurationService>();
builder.Services.AddScoped<ICashManagementService, CashManagementService>();
builder.Services.AddSingleton<IRfidReader, Windows10RfidReader>();
builder.Services.AddHostedService<RfidBackgroundService>();

// Registrar casos de uso
builder.Services.AddTransient<EnterCardUseCase>();
builder.Services.AddTransient<EnterPinUseCase>();
builder.Services.AddScoped<CheckBalance>();
builder.Services.AddScoped<Deposit>();
builder.Services.AddScoped<Transfer>();
builder.Services.AddScoped<Withdraw>();

builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddControllers();

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

app.MapHub<CardNotificationHub>("/cardNotificationHub");

app.MapControllers();

app.Run();
