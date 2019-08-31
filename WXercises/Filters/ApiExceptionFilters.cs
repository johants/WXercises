using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;
using WXercises.Models;

namespace WXercises.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger _logger;

        public ApiExceptionFilter()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public override void OnException(ExceptionContext exceptionContext)
        {
            var exception = exceptionContext.Exception;
            
            exceptionContext.Result = (IActionResult) new JsonResult((object) new ErrorResponse()
            {
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = (exception?.Message ?? "Sorry, something went wrong.")
            });

            _logger.Error(exception, exception.Message, exception.StackTrace);
        }
    }
}
