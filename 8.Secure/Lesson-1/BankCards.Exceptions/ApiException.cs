using System.Net;

namespace BankCards.Exceptions;
public class ApiException : Exception
{
    public HttpStatusCode Code { get; init; }
    public object Errors { get; init; }

    public ApiException(HttpStatusCode code, object errors = null)
    {
        Code = code;
        Errors = errors;
    }
}
