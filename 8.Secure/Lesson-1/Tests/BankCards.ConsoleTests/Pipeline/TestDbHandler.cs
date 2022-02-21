using BankCards.DAL.Context;
using BankCards.Domain;
using BankCards.Domain.Account;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Orlfi.CommonLibrary.Pipeline.Base;
using Orlfi.CommonLibrary.Pipeline.Interfaces;

namespace BankCards.ConsoleTests.Pipeline;

public class TestDbHandler : AbstractPipelineHandler<TestResult>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger _logger;

    public TestDbHandler(IServiceProvider serviceProvider, ILogger<TestDbHandler> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override bool Handle(IPipelineContext<TestResult> context)
    {
        throw new NotImplementedException();
    }

    protected override async Task<bool> HandleAsync(IPipelineContext<TestResult> context, CancellationToken cancel = default)
    {
        var cardsTask = GetCardsFromDb();
        var usersTask = GetUsersFromDb();
        await Task.WhenAll(cardsTask, usersTask).ConfigureAwait(false);

        var result = new List<string>();

        context.Data.CardsFromDb = cardsTask.Result;
        context.Data.UsersFromDb = usersTask.Result;
        return true;
    }

    private async Task<IEnumerable<Card>> GetCardsFromDb()
    {
        var scope = _serviceProvider.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<BankContext>();
        var cards = await db.Cards.ToArrayAsync().ConfigureAwait(false);
        return cards;
    }

    private async Task<IEnumerable<AppUser>> GetUsersFromDb()
    {
        using var scope = _serviceProvider.CreateAsyncScope();
        var db = scope.ServiceProvider.GetRequiredService<BankContext>() as IdentityDbContext<AppUser>;
        var users = await db.Users.ToArrayAsync().ConfigureAwait(false); ;
        return users;

    }
}
