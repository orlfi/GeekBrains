namespace BankCards.Interfaces.Data.Base;

public interface IResult
{
    public bool Succeeded { get; }

    public IReadOnlyCollection<IErrorInformation> Errors { get; }
}