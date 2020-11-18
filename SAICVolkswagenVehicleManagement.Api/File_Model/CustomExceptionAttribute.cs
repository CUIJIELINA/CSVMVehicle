using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace ResultTest
{
    public class CustomExceptionAttribute : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            //处理各种异常
            context.ExceptionHandled = true;
        }
    }
}
