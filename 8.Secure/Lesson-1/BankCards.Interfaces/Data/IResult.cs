using BankCards.Interfaces.Data.Base;

namespace BankCards.Interfaces.Data;

public interface IResult<TData> : IResult
{
    TData Data { get; }
}
