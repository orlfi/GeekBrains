using BankCards.DAL.Repositories;
using BankCards.Interfaces.Repositories;
using BankCards.ApiOrm.Extensions;
using Microsoft.AspNetCore.Identity;
using BankCards.DAL.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection.Extensions;
using BankCards.DAL;
using BankCards.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BankCards.Domain.Account;
using BankCards.Services;
using BankCards.Interfaces.Security;
using BankCards.Security;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;
using FluentValidation.AspNetCore;
using FluentValidation;
using BankCards.ApiOrm.DTO.Cards;
using BankCards.ApiOrm.Validators;
using BankCards.ApiOrm.Mappings;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddDatabase(builder.Configuration);
services.AddScoped<ICardRepository, CardsRepositoryOrm>();
services.AddScoped<IAccountManager, AccountManager>();
services.AddScoped<IDbInitializer, DbInitializer>();
services.AddScoped<IJwtGenerator, JwtGenerator>();
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddValidators();
services.AddMappers();

#region Добавляем аутентификацию и авторизацию
services.TryAddSingleton<ISystemClock, SystemClock>();
var identityBuilder = services.AddIdentityCore<AppUser>()
    .AddEntityFrameworkStores<BankContext>()
    .AddSignInManager<SignInManager<AppUser>>();
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"]));
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = key,
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateLifetime = true,
        ClockSkew = new TimeSpan(0), // убираю 5 минутное смещение к ValidateLifetime, задержка задается при создании токена
    };
});
// Требование аутентификации для всех запросов
services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

services.Configure<IdentityOptions>(opt =>
{
    if (builder.Environment.IsDevelopment())
    {
        opt.Password.RequireDigit = false;
        opt.Password.RequireLowercase = false;
        opt.Password.RequireUppercase = false;
        opt.Password.RequireNonAlphanumeric = false;
        opt.Password.RequiredLength = 3;
        opt.Password.RequiredUniqueChars = 3;
    }

    opt.User.RequireUniqueEmail = false;
    opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIGKLMNOPQRSTUVWXYZ1234567890";

    opt.Lockout.AllowedForNewUsers = false;
    opt.Lockout.MaxFailedAccessAttempts = 10;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
});
#endregion

services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BankCards", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    // c.IncludeXmlComments();
});


var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    await dbInitializer.InitializeAsync(
        builder.Configuration.GetValue<bool>("RemoveDatabase"),
        builder.Configuration.GetValue<bool>("InitializeDatabaseWithTestData"));
}

// Используем middleware обработчик ошибок для всех запросов
app.UseErrorHandling(app.Environment.IsDevelopment());

// Включаем поддержку Sawagger в зависимости от настроки параметра UseSwagger
if (app.Configuration.GetValue<bool>("UseSwagger"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BankCards Cards API V1");
        // Задаем Sawagger страницей по умолчанию в зависимости от настроки параметра SetSwaggerDefaultPage
        if (app.Configuration.GetValue<bool>("SetSwaggerDefaultPage"))
            c.RoutePrefix = "";
    });
}

app.UseHttpsRedirection();

if (!app.Environment.IsDevelopment())
{
    app.UseAuthentication();

    app.UseAuthorization();
}

app.MapControllers();

app.Map("/test", () => "Тестовая страница");

app.Run();


