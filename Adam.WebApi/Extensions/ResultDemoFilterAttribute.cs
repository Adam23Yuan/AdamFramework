using System.Net;
using Adam.WebApi.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Adam.WebApi.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public class ResultDemoFilterAttribute : ResultFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is JsonResult)
            {
                JsonResult result = (JsonResult)context.Result;

                var resultJson = ResponseModelBuilder.Build((int)HttpStatusCode.OK, "", result.Value);
                context.Result = new JsonResult(resultJson);
            }
            if (context.Result is ObjectResult)
            {
                ObjectResult result = (ObjectResult)context.Result;

                var resultJson = ResponseModelBuilder.Build((int)HttpStatusCode.OK, "", result.Value);
                context.Result = new JsonResult(resultJson);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            return base.OnResultExecutionAsync(context, next);
        }
    }
}
