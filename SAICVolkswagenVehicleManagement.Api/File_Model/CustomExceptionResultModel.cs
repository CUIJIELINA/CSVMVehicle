using System;

namespace ResultTest.Models
{
    public class CustomExceptionResultModel : BaseResultModel
    {
        public CustomExceptionResultModel(int? code, Exception exception)
        {
            Code = code;
            Message = exception.InnerException != null ?
                exception.InnerException.Message :
                exception.Message;
            Result = exception.Message;
            RetureStates = RetureStates.Error;
        }
    }
}
