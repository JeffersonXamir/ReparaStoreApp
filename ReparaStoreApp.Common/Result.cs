    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Common
{
    public class Result<T>
    {
        public bool Success { get; }
        public T Data { get; }
        public string Error { get; }

        public Result(bool success, T data, string error)
        {
            Success = success;
            Data = data;
            Error = error;
        }
    }
}
