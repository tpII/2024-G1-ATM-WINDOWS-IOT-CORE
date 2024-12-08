using ATM.Infrastructure.Repositories;
using ATM.Application.Interfaces.Repositories;
using ATM.Application.UseCases;

var builder = WebApplication.CreateBuilder(args);


// Registrar el caso de uso
builder.Services.AddTransient<CheckBalance>();

// Configurar el repositorio con `HttpClient`
builder.Services.AddHttpClient<IAccountRepository, AccountRepository>(client =>
{
    client.BaseAddress = new Uri("http://192.168.0.148:5000"); // IP del servidor en la PC
});


// Add services to the container.
builder.Services.AddRazorPages();

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
