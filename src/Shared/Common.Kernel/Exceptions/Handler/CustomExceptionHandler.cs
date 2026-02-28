using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;


namespace Common.Kernel.Exceptions.Handler
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var (statusCode, title, detail) = exception switch
            {
                NotFoundException nf => (
                    StatusCodes.Status404NotFound,
                    "Запрашиваемый ресурс не найден",
                    nf.Message

                ),
                ValidationException vf => (
                    StatusCodes.Status400BadRequest,
                    "Ошибка валидации",
                    vf.Message

                ),
                _=>(
                    StatusCodes.Status500InternalServerError,
                    "Внутренняя ошибка сервера",
                    "Произошла непредвиденная ошибка"
                )
            };

            httpContext.Response.StatusCode = statusCode;

            var problemDetails = new ProblemDetails
            {
                Title = title,
                Detail = detail,
                Status = statusCode,
                Instance = httpContext.Request.Path
            };

            problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

            if(exception is ValidationException fvException)
            {
                var errors = fvException.Errors?
                    .Select(x => new {x.PropertyName, x.ErrorMessage})
                    .ToList();

                if (errors is not null && errors.Count > 0)
                {
                    problemDetails.Extensions.Add("ValidationException", errors);
                }
            } else
            {
                problemDetails.Extensions.Add("errorMessage", exception.Message);
            }

                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
