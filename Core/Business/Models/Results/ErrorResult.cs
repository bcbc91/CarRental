using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Business.Models.Results
{
    public class ErrorResult:Result
    {
        public ErrorResult(string message):base(ResultStatus.Error,message)
        {
            
        }

        public ErrorResult():base(ResultStatus.Error)
        {
            
        }
    }
}
