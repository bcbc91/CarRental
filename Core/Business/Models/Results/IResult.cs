using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Business.Models.Results
{
    public interface IResult
    {
        ResultStatus Status { get; }
        string Message { get; }
    }
}
