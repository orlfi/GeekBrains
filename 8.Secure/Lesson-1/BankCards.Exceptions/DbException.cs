using System.Net;

namespace BankCards.Exceptions;
public class DbException : Exception
{
    public HttpStatusCode Code { get; init; }
    public object Errors { get; init; }

    public DbException(HttpStatusCode code, object errors = null)
    {
        Code = code;
        Errors = errors;
    }
}
