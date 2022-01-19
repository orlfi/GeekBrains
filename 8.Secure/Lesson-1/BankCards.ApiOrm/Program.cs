using BankCards.DAL.Context;
using BankCards.DAL.Repositories;
using BankCards.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
//services.AddDbContext<BankContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("default")));
services.AddDbContext<BankContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("postgres")));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

services.AddScoped<ICardRepository, CardsRepositoryOrm>();

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
