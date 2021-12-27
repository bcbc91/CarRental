using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Business.Models.Results
{
    public class SuccessDataResult<T>:DataResult<T>
    {
        public SuccessDataResult(T data,string message):base(data,ResultStatus.Success,message)
        {
            
        }

        public SuccessDataResult(string message):base(default,ResultStatus.Success,message)
        {
            
        }

        public SuccessDataResult(T data):base(data,ResultStatus.Success)
        {
            
        }

        public SuccessDataResult():base(default,ResultStatus.Success)
        {
            
        }
    }
}
