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

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.AddDatabase(builder.Configuration);
services.AddScoped<ICardRepository, CardsRepositoryOrm>();
services.AddScoped<IAccountManager, AccountManager>();
services.AddScoped<IDbInitializer, DbInitializer>();
services.AddScoped<IJwtGenerator, JwtGenerator>();
services.AddControllers();
services.AddEndpointsApiExplorer();

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
        ClockSkew = new TimeSpan(0), // убираю 5 минутное смещение к ValidateLifetime
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
#if DEBUG
    opt.Password.RequireDigit = false;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 3;
    opt.Password.RequiredUniqueChars = 3;
#endif

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
});


var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    await dbInitializer.InitializeAsync(false);
}

// Используем middleware обработчик ошибок для всех запросов
app.UseErrorHandling(app.Environment.IsDevelopment());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Map("/test", () => "Тестовая страница");

app.Run();


