using HomitagChallenge.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomitagChallenge.Services.ViewModels
{
    public class Response<T> : IResponse<T> where T : class
    {
        public bool IsSuccess { get; private set; }

        public T Data { get; private set; }

        public string Message { get; private set; }

        /// <summary>
        /// Set success response
        /// </summary>
        /// <param name="successMessage"></param>
        /// <param name="result"></param>
        public void Success(string successMessage, T result = default(T))
        {
            this.IsSuccess = true;

            this.Data = result;

            this.Message = successMessage;
        }

        /// <summary>
        /// Set failure response
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="result"></param>
        public void Failure(string errorMessage, T result = default(T))
        {
            this.IsSuccess = false;

            this.Data = result;

            this.Message = errorMessage;
        }
    }
}
