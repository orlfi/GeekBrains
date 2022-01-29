using BankCards.ConsoleTests.DTO.Cards;
using BankCards.ConsoleTests.Mappings;
using BankCards.Domain;
using BankCards.Interfaces.Data.Account;
using BankCards.Services.DTO;
using Microsoft.Extensions.Logging;
using Orlfi.CommonLibrary.Pipeline.Base;
using Orlfi.CommonLibrary.Pipeline.Interfaces;
using System.Net;
using System.Net.Http.Json;

namespace BankCards.ConsoleTests.Pipeline;

public class TestWebApiHandler : AbstractPipelineHandler<TestResult>
{
    private readonly HttpClient _client;
    private readonly ILogger _logger;
    private string? _token = null;
    private readonly ILoginRequest _loginRequest = null!;

    public TestWebApiHandler(HttpClient client, LoginRequest loginRequest, ILogger<TestWebApiHandler> logger)
    {
        _client = client;
        _loginRequest = loginRequest;
        _logger = logger;
    }

    protected override bool Handle(IPipelineContext<TestResult> context)
    {
        return HandleAsync(context).Result;
    }

    protected override async Task<bool> HandleAsync(IPipelineContext<TestResult> context, CancellationToken cancel = default)
    {
        try
        {
            Console.WriteLine("\r\nИз API:");
            context.Data.CardsFromApi = await GetCardsFromApi(cancel);
            return true;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError("Ошибка сервера {0}", ex.StatusCode);
            throw;
        }
        catch
        {
            throw;
        }
    }

    private async Task Authenticate(CancellationToken cancel = default)
    {
        System.Net.ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
        using var response = await _client.PostAsJsonAsync("api/account/login", _loginRequest, cancel).ConfigureAwait(false);
        var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>(cancellationToken: cancel).ConfigureAwait(false);
        _token = loginResponse?.Token;
        _client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _token);
    }

    private async Task<IEnumerable<Card>> GetCardsFromApi(CancellationToken cancel = default)
    {
        _logger.LogInformation("Запрашиваем информацию о картах с WEB API...");
        if (string.IsNullOrEmpty(_token))
            await Authenticate(cancel).ConfigureAwait(false);

        var cards = await _client.GetFromJsonAsync<ICollection<CardResponse>>("api/cards", cancellationToken: cancel).ConfigureAwait(false);

        if (cards is null)
            throw new NullReferenceException("Список карточек не может быть null");
        _logger.LogInformation("Получено {0} записей", cards.Count);
        return cards.ToCard();
    }
}
