using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Business.Models.Results
{
    public class ErrorDataResult<T>:DataResult<T>
    {
        public ErrorDataResult(T data, string message) : base(data, ResultStatus.Error, message)
        {

        }

        public ErrorDataResult(string message) : base(default, ResultStatus.Error, message)
        {

        }

        public ErrorDataResult(T data) : base(data, ResultStatus.Error)
        {

        }

        public ErrorDataResult() : base(default, ResultStatus.Error)
        {

        }
    }
}
