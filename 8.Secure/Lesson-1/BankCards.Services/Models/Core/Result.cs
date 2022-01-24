using BankCards.Interfaces.Data;
using BankCards.Interfaces.Data.Base;

namespace BankCards.Domain.Core;

public readonly struct Result : IResult
{
    public bool Succeeded => Errors.Count > 0;
    public IReadOnlyCollection<IErrorInformation> Errors { get; }

    public Result() : this(new List<IErrorInformation>())
    {
    }

    public Result(IErrorInformation error) : this(new List<IErrorInformation>() { error })
    {
    }

    public Result(IReadOnlyCollection<IErrorInformation> errors)
    {
        Errors = errors;
    }

}
