using Microsoft.AspNetCore.Mvc;
using System;

namespace ResultTest.Models
{
    public class CustomExceptionResult : ObjectResult
    {
        public CustomExceptionResult(int? code, Exception exception)
                : base(new CustomExceptionResultModel(code, exception))
        {
            StatusCode = code;
        }
    }
}
