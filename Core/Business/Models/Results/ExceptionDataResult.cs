using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Business.Models.Results
{
    public class ExceptionDataResult<T> : DataResult<T>
    {
        public ExceptionDataResult(T data,Exception exception, bool showException = true)
            : base(default,ResultStatus.Exception,
                showException == true ?
                    (exception != null ? "Exception: " + exception.Message +
                                         (exception.InnerException != null ? " | Inner Exception: " + exception.InnerException.Message+
                                                                             (exception.InnerException.InnerException != null ? " | " + exception.InnerException.InnerException.Message : "")
                                             : "")
                        : "")
                    : "Exception"
                )
        {

        }

        public ExceptionDataResult() : base(default,ResultStatus.Exception, "Exception")
        {

        }
    }
}
