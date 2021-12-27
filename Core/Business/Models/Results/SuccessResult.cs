using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Business.Models.Results
{
    public class SuccessResult:Result
    {
        public SuccessResult(string message):base(ResultStatus.Success,message)
        {
            
        }

        public SuccessResult():base(ResultStatus.Success)
        {
            
        }
    }
}
