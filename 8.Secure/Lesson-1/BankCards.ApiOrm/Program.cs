using BankCards.DAL.Repositories;
using BankCards.Interfaces.Repositories;
using BankCards.ApiOrm.Extensions;
using BankCards.Domain;
using Microsoft.AspNetCore.Identity;
using BankCards.DAL.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddDatabase(builder.Configuration);

services.TryAddSingleton<ISystemClock, SystemClock>();
var identityBuilder = services.AddIdentityCore<AppUser>();
identityBuilder.AddEntityFrameworkStores<BankContext>();
identityBuilder.AddSignInManager<SignInManager<AppUser>>();
services.AddScoped<ICardRepository, CardsRepositoryOrm>();
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();
app.MigrateDatabase();
// Используем обработчик ошибок для всех запросов
app.UseErrorHandling(app.Environment.IsDevelopment());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();


