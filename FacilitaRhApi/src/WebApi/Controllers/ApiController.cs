using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FacilitaRhApi.Domain.Abstractions;

namespace FacilitaRhApi.WebApi.Controllers
{
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
        protected IActionResult HandleFailure(Result result)
        {
            if (result.Error == null)
                return StatusCode(500, "An unknown error occurred.");

            return result.Error.Type switch
            {
                ErrorType.NotFound => NotFound(result.Error.Description),
                ErrorType.Validation => BadRequest(result.Error.Description),
                _ => StatusCode(500, result.Error.Description)
            };
        }
    }
}
