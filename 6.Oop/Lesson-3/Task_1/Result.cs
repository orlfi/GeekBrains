using System.Collections.Generic;

namespace Task_1
{
    public ref readonly struct Result<T>
    {
        public bool Success { get; set; }

        public T Data { get; set; }

        public string Error { get; set; }
    }
}