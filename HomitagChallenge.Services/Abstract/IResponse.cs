using System;
using System.Collections.Generic;
using System.Text;

namespace HomitagChallenge.Services.Abstract
{
    public interface IResponse<T> where T : class
    {
        bool IsSuccess { get; }
        T Data { get; }
        string Message { get; }
        void Success(string successMessage, T result = default(T));
        void Failure(string errorMessage, T result = default(T));
    }
}
