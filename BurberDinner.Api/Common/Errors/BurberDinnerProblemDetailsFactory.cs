using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace BurberDinner.Api.Common.Errors
{
    public class BurberDinnerProblemDetailsFactory : ProblemDetailsFactory
    {
        private readonly ApiBehaviorOptions _options;

        public BurberDinnerProblemDetailsFactory(IOptions<ApiBehaviorOptions> options)
        {
            _options = options.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public override ProblemDetails CreateProblemDetails(
            HttpContext httpContext, 
            int? statusCode = null, 
            string? title = null, 
            string? type = null, 
            string? detail = null, 
            string? instance = null)
        {
            statusCode ??= 500; // Default to 500 if statusCode is not provided

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title ?? "An error occurred",
                Type = type,
                Detail = detail,
                Instance = instance
            };

            ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);

            return problemDetails;
        }

        public override ValidationProblemDetails CreateValidationProblemDetails(
            HttpContext httpContext, 
            ModelStateDictionary modelStateDictionary, 
            int? statusCode = null, 
            string? title = null, 
            string? type = null, 
            string? detail = null, 
            string? instance = null)
        {
            statusCode ??= 400; // Default to 400 if statusCode is not provided

            var validationProblemDetails = new ValidationProblemDetails(modelStateDictionary)
            {
                Status = statusCode,
                Title = title ?? "One or more validation errors occurred.",
                Type = type,
                Detail = detail,
                Instance = instance
            };

            ApplyProblemDetailsDefaults(httpContext, validationProblemDetails, statusCode.Value);

            return validationProblemDetails;
        }

        private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int statusCode)
        {
            problemDetails.Status = statusCode;
            problemDetails.Instance = httpContext?.Request?.Path;

            if (_options.ClientErrorMapping.TryGetValue(statusCode, out var clientErrorData))
            {
                problemDetails.Title ??= clientErrorData.Title;
                problemDetails.Type ??= clientErrorData.Link;
            }
        }
    }
}
