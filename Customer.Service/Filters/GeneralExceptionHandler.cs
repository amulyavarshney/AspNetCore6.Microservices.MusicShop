using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Order.Service.Exceptions;

namespace Order.Service.Filters
{
    public class GeneralExceptionHandler : IActionFilter, IOrderedFilter
    {
        public int Order { get; set; }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is RecordNotFoundException)
            {
                context.Result = new ObjectResult(new
                {
                    context.Exception.Message
                })
                {
                    StatusCode = 400
                };
                context.ExceptionHandled = true;
            }
            if (!context.ExceptionHandled && context.Exception != null)
            {
                context.Result = new OkObjectResult(new
                {
                    context.Exception.Message
                })
                {
                    StatusCode = 503
                };
                context.ExceptionHandled = true;
            }
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
