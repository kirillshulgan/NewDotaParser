using DotaParser.Application;
using DotaParser.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Подключаем наши слои с помощью методов расширений
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

// Настройка авторизации Steam
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LoginPath = "/api/auth/login"; // Сюда перенаправим, если нет доступа
})
.AddSteam(options =>
{
    options.ApplicationKey = builder.Configuration["Steam:ApplicationKey"];
    options.CallbackPath = "/signin-steam"; // Стандартный путь Steam пакета
});

// Настройка Swagger для тестирования API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Включаем Swagger всегда для удобства разработки
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();