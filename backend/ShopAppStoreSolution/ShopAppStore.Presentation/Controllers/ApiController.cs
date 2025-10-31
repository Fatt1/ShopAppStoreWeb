using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopAppStore.Shared;

namespace ShopAppStore.Presentation.Controllers
{

    public abstract class ApiController : ControllerBase
    {
        protected readonly ISender _sender;
        protected ApiController(ISender sender)
        {
            _sender = sender;
        }

        protected IActionResult HandleFailer(Result result) =>
            result switch
            {
                { IsSuccess: true } => throw new InvalidOperationException(),
                _ => BadRequest(CreateProblemDetail("Bad Request", StatusCodes.Status400BadRequest, result.Error))
            };

        private static ProblemDetails CreateProblemDetail(string title, int status, Error error, Error[]? errors = null) => new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), error } },
        };
    }
}
