using System;
using System.Collections.Generic;

namespace Task_1
{
    public class Result<T>
    {
        public bool Success => Error is null;

        public T Data { get; set; }

        public Exception Error { get; set; }

        public static implicit operator Result<T>(T value) => new Result<T>() { Data = value };

        public static implicit operator Result<T>(Exception exception) => new Result<T>() { Error = exception };

        public static implicit operator Result<T>((T value, Exception exception) t) => new Result<T>() { Data = t.value, Error = t.exception };

        public static implicit operator T(Result<T> result) => result.Data;

        public static implicit operator Exception(Result<T> result) => result.Error;
    }
}