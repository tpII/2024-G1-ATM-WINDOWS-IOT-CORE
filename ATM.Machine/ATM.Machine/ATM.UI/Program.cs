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
using System.Net;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://0.0.0.0:5000"); // Escuchar en todas las interfaces

// Verificar si se proporcionó al menos un argumento
if (args.Length < 2)
{
    Console.Error.WriteLine("Error: No se proporcionó una dirección IP y un port");
    Environment.Exit(1); // Salir con código de error 1
}

string ip = args[0];

// Validar si el argumento es una dirección IP válida
if (!IPAddress.TryParse(ip, out _))
{
    Console.Error.WriteLine($"Error: La dirección IP proporcionada '{ip}' no es válida.");
    Environment.Exit(1); // Salir con código de error 1
}

string port = args[1];

Console.WriteLine($"La dirección IP es válida: {ip}" + $" y su port es {port}");

// var uri = new Uri("http://localhost:5010");
var uri = new Uri($"http://{ip}:{port}");

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

// Registrar servicios
builder.Services.AddTransient<IEncryptionService, EncryptionService>();
builder.Services.AddSingleton<SessionService>();
builder.Services.AddSingleton<ATMConfigurationService>();
builder.Services.AddSingleton<CashManagementService>();
builder.Services.AddScoped<IAccountService, AccountService>();
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

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontendMERN", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

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

// Configuración de CORS en la aplicación
app.UseCors("AllowFrontendMERN");

app.UseAuthorization();

app.MapRazorPages();

app.MapHub<CardNotificationHub>("/cardNotificationHub");

app.MapControllers();

app.Run();
