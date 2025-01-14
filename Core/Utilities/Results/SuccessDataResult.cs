﻿namespace Core.Utilities.Results
{
    /// <summary>
    /// Veri dönen başarılı sonuçlar için kullanılır.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SuccessDataResult<T> : DataResult<T>
    {
        public SuccessDataResult(T data, bool success, string message) : base(data, success, message)
        {
        }
        public SuccessDataResult(T data, string message) : base(data, true, message)
        {

        }
        public SuccessDataResult(T data) : base(data, true)
        {
        }
        //public SuccessDataResult(string message) : base(default,true,message)
        //{
        //}
        public SuccessDataResult() : base(default, true)
        {
        }
    }
}
