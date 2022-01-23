using BankCards.Interfaces.Data;
using BankCards.Services.Models;
using System.Collections.ObjectModel;

namespace BankCards.Domain.Core;

public readonly struct Result<TData> : IResult<TData>
{
    public bool Succeeded => Errors.Count == 0;

    public IReadOnlyCollection<IErrorInformation> Errors { get; } 

    public TData Data { get; }

    public Result(TData data) : this(data, new List<IErrorInformation>()) { }

    public Result(IErrorInformation error) : this(default!, new List<IErrorInformation>() { error}) { }

    public Result(IReadOnlyCollection<IErrorInformation> errors) : this(default!, errors) { }

    public Result(TData data, IReadOnlyCollection<IErrorInformation> errors)
    {
        Data = data;
        Errors = errors;
    }

    public static implicit operator Result<TData>(TData data) => new Result<TData>(data);

    public static implicit operator Result<TData>(ErrorInformation error) => new Result<TData>(error);

    public static implicit operator Result<TData>(List<ErrorInformation> errors) => new Result<TData>(errors);
    
    public static implicit operator Result<TData>(ErrorInformation[] errors) => new Result<TData>(new ReadOnlyCollection<ErrorInformation>(errors));

    public static implicit operator TData(Result<TData> result) => result.Data;

    public static implicit operator ErrorInformation(Result<TData> result) => (ErrorInformation)result.Errors.FirstOrDefault()!;

    public static implicit operator ReadOnlyCollection<ErrorInformation>(Result<TData> result) => (ReadOnlyCollection<ErrorInformation>)result.Errors;

}
