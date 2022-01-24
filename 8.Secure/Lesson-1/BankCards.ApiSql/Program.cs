using BankCards.DAL;
using BankCards.DAL.Context;
using BankCards.DAL.Repositories;
using BankCards.Interfaces;
using BankCards.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
services.AddDbContext<BankContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("default")));

services.AddScoped<IConnectionManager, ConnectionManager>();
services.AddScoped<ICardRepository, CardsRepositorySql>();

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
