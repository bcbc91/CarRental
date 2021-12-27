using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;

namespace Core.Business.Models.Results
{
    public class DataResult<T>:Result,IDataResult<T>
    {
     

        public T Data { get; }

        public DataResult(T data, ResultStatus status,string message):base(status,message)
        {
            Data = data;
        }

        public DataResult(T data,ResultStatus resultStatus):base(ResultStatus.Success)
        {
            Data = data;
        }
    }
}
