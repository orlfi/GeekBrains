using BankCards.DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BankCards.ConsoleTests;

public class Application
{
    private static string _path = null!;
    public static string Path => _path ??= System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!;

    private readonly ILogger _logger;
    private readonly BankContext _db;

    public Application(BankContext db, ILogger<Application> logger)
    {
        _logger = logger;
        _db = db;
    }

    public async Task RunAsync()
    {
        try
        {
            _logger.LogInformation("Application started");

            await InitData().ConfigureAwait(false);

            var items = await _db.Cards.ToArrayAsync().ConfigureAwait(false); ;
            foreach (var item in items)
            {
                Console.WriteLine($"{item.Id,3} {item.Number,16}");
            }
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "Необработанная ошибка {0}", ex.Message);
        }
    }

    private async Task InitData()
    {
        if (await _db.Cards.AnyAsync().ConfigureAwait(false))
            return;

        var card = await _db.Cards.AddAsync(new Domain.Card()
        {
            Number = "1111222233334444",
            Cvc = 111,
            Created = DateTime.Now,
            ValidThru = DateTime.Now.AddYears(5),
            Owner = "TestOwner",
            Type = 0
        }).ConfigureAwait(false); ;
        await _db.SaveChangesAsync().ConfigureAwait(false);
    }
}