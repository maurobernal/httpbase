using HttpBase.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HttpBase.Filters
{
    public class ExceptionFilterGeneric : IExceptionFilter
    {
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public ExceptionFilterGeneric(
            IModelMetadataProvider modelMetadataProvider)
        {
            _modelMetadataProvider = modelMetadataProvider;
        }

        public void OnException(ExceptionContext context)
        {
            var details = new ProblemDetails
            {
                Title = context.ActionDescriptor.DisplayName,
                Type = "https://tools.ietf.org/html/rfc7231",
                Detail = context.Exception.Message,
                Status = StatusCodes.Status500InternalServerError,
            };

            if (context.Exception.GetType() == typeof(Exception_API))
            {
                var excep = (Exception_API)context.Exception;
                details.Status = excep.code;
            }

            var result = new ViewResult { ViewName = "ErrorCustom" };
            result.ViewData = new ViewDataDictionary(
                _modelMetadataProvider,
                context.ModelState);
            result.ViewData["Error"] = details;
            context.Result = result;
        }
    }
}
