using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using BankCards.ConsoleTests.DTO.Cards;
using BankCards.ConsoleTests.Mappings;
using BankCards.DAL.Context;
using BankCards.Domain;
using BankCards.Domain.Account;
using BankCards.Interfaces.Data.Account;
using BankCards.Services.DTO;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BankCards.ConsoleTests;

public class Application
{
    private static string _path = null!;
    public static string Path => _path ??= System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)!;

    private readonly HttpClient _client;
    private readonly IServiceProvider _services;
    private readonly ILogger _logger;
    private readonly BankContext _db;
    private readonly ILoginRequest _loginRequest;

    private string? _token = null;

    public Application(HttpClient client, IServiceProvider services, BankContext db, IOptions<LoginRequest> loginOptions, ILogger<Application> logger)
    {
        _client = client;
        _services = services;
        _logger = logger;
        _db = db;
        _loginRequest = loginOptions.Value;
    }

    public async Task RunAsync()
    {
        try
        {
            _logger.LogInformation("Application started");
            await PrintInfoFromDb().ConfigureAwait(false);
            await PrintInfoFromApi().ConfigureAwait(false);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(ex, "Необработанная ошибка {0}", ex.Message);
        }
    }

    private async Task Authenticate(CancellationToken cancel = default)
    {
        using var response = await _client.PostAsJsonAsync("api/account/login", _loginRequest, cancel).ConfigureAwait(false);
        var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>(cancellationToken: cancel).ConfigureAwait(false);
        _token = loginResponse?.Token;
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _token);
    }

    private async Task PrintInfoFromApi()
    {
        try
        {
            // var cardsTask = GetCardsFromApi().ConfigureAwait(false);
            // var cards = await cardsTask;
            Console.WriteLine("Из API:");
            await CheckTokenExpiration();
            // PrintCards(cards);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError("Ошибка сервера {0}", ex.StatusCode);
        }
        catch
        {
            throw;
        }
    }

    private async Task CheckTokenExpiration(CancellationToken cancel = default)
    {
        int interation = 0;
        while (true)
        {
            cancel.ThrowIfCancellationRequested();
            var cards = await GetCardsFromApi(cancel).ConfigureAwait(false);
            Console.WriteLine($"Итерация {interation++}");
            PrintCards(cards);
            await Task.Delay(10000, cancel).ConfigureAwait(false);
        }
    }

    private async Task<IEnumerable<Card>> GetCardsFromApi(CancellationToken cancel)
    {
        if (string.IsNullOrEmpty(_token))
            await Authenticate(cancel).ConfigureAwait(false);

        var cards = await _client.GetFromJsonAsync<CardsResponse>("api/cards", cancellationToken: cancel).ConfigureAwait(false);

        if (cards is null)
            throw new NullReferenceException("Список карточек не может быть null");

        return cards.Cards.ToCard();
    }

    private async Task PrintInfoFromDb()
    {
        var cardsTask = GetCardsFromDb();
        var usersTask = GetUsersFromDb();
        await Task.WhenAll(cardsTask, usersTask).ConfigureAwait(false);

        Console.WriteLine("Из БД:");
        PrintCards(cardsTask.Result);
        PrintUsers(usersTask.Result);
    }

    private async Task<IEnumerable<Card>> GetCardsFromDb()
    {
        var scope = _services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<BankContext>();
        var cards = await db.Cards.ToArrayAsync().ConfigureAwait(false);
        return cards;
    }

    private void PrintCards(IEnumerable<Card> cards)
    {
        foreach (var item in cards)
        {
            Console.WriteLine($"{item.Id,3} {item.Number,16} {item.Owner,-20}");
        }
    }
    private async Task<IEnumerable<AppUser>> GetUsersFromDb()
    {
        using var scope = _services.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<BankContext>() as IdentityDbContext<AppUser>;
        var users = await db.Users.ToArrayAsync().ConfigureAwait(false); ;
        return users;

    }
    private void PrintUsers(IEnumerable<AppUser> users)
    {
        foreach (var item in users)
        {
            Console.WriteLine($"{item.UserName,-20} {item.Email,-30} {item.LastName}.{item.FirstName[0]}.{item.MiddleName[0]} ");
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