using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ResultTest.Models;

namespace ResultTest.Filters
{
    /// <summary>
    /// 结果过滤器
    /// </summary>
    public class ApiResultFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ValidationFailedResult)
            {
                ObjectResult objectResult = context.Result as ObjectResult;
                context.Result = objectResult;
            }
            else
            {
                ObjectResult objectResult = context.Result as ObjectResult;
                BaseResultModel model = new BaseResultModel(code: objectResult.StatusCode == null ? 200 : objectResult.StatusCode, result: objectResult.Value);
                #region      返回状态码以及对应的IActionResult格式和原因
                //200 OK 用于一般性的成功返回，不可用于请求错误返回
                //201 Created 资源被创建
                //202 Accepted 用于资源异步处理的返回，仅表示请求已经收到。对于耗时比较久的处理，一般用异步处理来完成
                //204 No Content  此状态可能会出现在 PUT、POST、DELETE 的请求中，一般表示资源存在，但消息体中不会返回任何资源相关的状态或信息
                //400 Bad Request 用于客户端一般性错误信息返回, 在其它 4xx 错误以外的错误，也可以使用，错误信息一般置于 body 中
                //401 Unauthorized 接口需要授权访问，为通过授权验证
                //403 Forbidden 当前的资源被禁止访问
                //404 Not Found   找不到对应的信息
                //500 Internal Server Error 服务器内部错误
                #endregion
                switch (model.Code)
                {
                    case 200:
                        model.Message = "返回成功";
                        break;
                    case 201:
                        model.Message = "资源被创建!";
                        break;
                    case 202:
                        model.Message = "用于资源异步处理的返回，仅表示请求已经收到。对于耗时比较久的处理，一般用异步处理来完成!";
                        break;
                    case 204:
                        model.Message = "此状态可能会出现在 PUT、POST、DELETE 的请求中，一般表示资源存在，但消息体中不会返回任何资源相关的状态或信息!";
                        break;
                    case 400:
                        model.Message = "用于客户端一般性错误信息返回, 在其它 4xx 错误以外的错误，也可以使用，错误信息一般置于 body 中!";
                        break;
                    case 401:
                        model.Message = "接口需要授权访问，为通过授权验证!";
                        break;
                    case 403:
                        model.Message = "当前的资源被禁止访问!";
                        break;
                    case 404:
                        model.Message = "找不到对应的信息!";
                        break;
                    case 500:
                        model.Message = "服务器内部错误!";
                        break;
                    default:
                        break;
                }
                context.Result = new OkObjectResult(model);
            }
        }
    }
}
