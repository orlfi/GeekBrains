namespace Timesheets.Infrastructure.Validation
{
    public interface IErrorCodes
    {
        string GetMessage(string key);
    }
}