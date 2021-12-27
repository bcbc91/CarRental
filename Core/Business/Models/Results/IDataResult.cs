using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Business.Models.Results
{
    public interface IDataResult<out T>:IResult
    {
        T Data { get; }
    }
}
