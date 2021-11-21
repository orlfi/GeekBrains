using System;
using System.Collections.Generic;

namespace Task_1
{
    public readonly ref struct Result<T>
    {
        public bool Success => Error is null;

        public T Data { get; }

        public Exception Error { get; }

        public static implicit operator Result<T>(T value) => new Result<T>(value);

        public static implicit operator Result<T>(Exception exception) => new Result<T>(exception);

        public static implicit operator Result<T>((T value, Exception exception) tuple) => new Result<T>(tuple.value, tuple.exception);

        public static implicit operator T(Result<T> result) => result.Data;

        public static implicit operator Exception(Result<T> result) => result.Error;

        public Result(T data) : this(data, null) { }

        public Result(Exception error) : this(default(T), error) { }

        public Result(T data, Exception error) => (Data, Error) = (data, error);
    }
}