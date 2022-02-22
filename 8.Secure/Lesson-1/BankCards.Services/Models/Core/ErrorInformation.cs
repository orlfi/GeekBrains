using BankCards.Interfaces.Data;

namespace BankCards.Services.Models;

public class ErrorInformation : IErrorInformation
{
    public string Message { get; }

    public ErrorInformation(string message)
    {
        Message = message;
    }
}
