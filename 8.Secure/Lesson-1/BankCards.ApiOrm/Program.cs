using BankCards.DAL.Repositories;
using BankCards.Interfaces.Repositories;
using BankCards.ApiOrm.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddDatabase(builder.Configuration);
services.AddScoped<ICardRepository, CardsRepositoryOrm>();
services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();
// Используем обработчик ошибок для всех запросов
app.UseErrorHandling(app.Environment.IsDevelopment());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


