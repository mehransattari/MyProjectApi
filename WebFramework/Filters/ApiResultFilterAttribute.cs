
using Common.ResultStatus;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebFramework.Api;

namespace WebFramework.Filters;

public class ApiResultFilterAttribute : ActionFilterAttribute
{
    /// <summary>
    /// با این فیلتر اگر خروجی اشکن ریزالت بزاریم خودش اتومات تبدیل میکنه به ای پی ریزالت
    /// </summary>
    /// <param name="context"></param>
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        if(context.Result is OkObjectResult okObjectResult)
        {
            var apiResult = new ApiResult<object>(true, ApiResultStatusCode.Success, okObjectResult.Value);
            context.Result = new JsonResult(apiResult) { StatusCode = okObjectResult.StatusCode };
        }

        else if(context.Result is OkResult okResult)
        {
            var apiResult = new ApiResult(true, ApiResultStatusCode.Success); 
            context.Result = new JsonResult(apiResult) { StatusCode = okResult.StatusCode };  
        }

        else if(context.Result is BadRequestResult badRequestResult)
        {
            var apiResult = new ApiResult(false, ApiResultStatusCode.BadRequest);
            context.Result = new JsonResult(apiResult) { StatusCode = badRequestResult.StatusCode};
        }

        else if(context.Result is BadRequestObjectResult badRequestObjectResult)
        {
            var message = badRequestObjectResult.Value.ToString();
            if(badRequestObjectResult.Value is SerializableError erorrs)
            {
                var errorMessages = erorrs.SelectMany(x => (string[])x.Value).Distinct();
                message = string.Join("|", errorMessages);
            }
            var apiResult = new ApiResult(false, ApiResultStatusCode.BadRequest, message);
            context.Result = new JsonResult(apiResult) { StatusCode = badRequestObjectResult.StatusCode};
        }

        else if(context.Result is ContentResult contentResult)
        {
            var apiResult = new ApiResult(true, ApiResultStatusCode.Success, contentResult.Content);
            context.Result = new JsonResult(apiResult) { StatusCode = contentResult.StatusCode};
        }

        else if(context.Result is NotFoundResult notFoundResult )
        {
            var apiResult = new ApiResult(false, ApiResultStatusCode.NotFound);
            context.Result = new JsonResult(apiResult) { StatusCode =notFoundResult.StatusCode};
        }

        else if(context.Result is NotFoundObjectResult notFoundObjectResult)
        {
            var apiResult = new ApiResult<object>(false, ApiResultStatusCode.NotFound, notFoundObjectResult.Value);
            context.Result = new JsonResult(apiResult) { StatusCode = notFoundObjectResult.StatusCode};
        }

        else if(context.Result is ObjectResult objectResult && 
                                               objectResult.StatusCode == null && 
                                             !(objectResult.Value is ApiResult))
        {
            var apiResult = new ApiResult<object>(false, ApiResultStatusCode.Success, objectResult.Value);
            context.Result = new JsonResult(apiResult) { StatusCode = objectResult.StatusCode};
        }

        base.OnResultExecuting(context);    
    }
}
