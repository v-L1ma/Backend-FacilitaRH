using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FacilitaRhApi.Application.Features.Users.CreateUser;
using FacilitaRhApi.Application.Features.Users.AuthenticateUser;
using FacilitaRhApi.Application.Features.Users.GetUsers;

namespace FacilitaRhApi.WebApi.Controllers;

[Route("users")]
public class UsersController : ApiControllerBase
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return HandleFailure(result);

        return Ok(new { user = new { id = result.Value } });
    }

    [HttpPost("auth")]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserCommand command)
    {
        var result = await _sender.Send(command);
        if (!result.IsSuccess)
            return HandleFailure(result);

        return Ok(new
        {
            user = new { id = result.Value!.Id, email = result.Value.Email },
            token = result.Value.Token
        });
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _sender.Send(new GetUsersQuery());
        if (!result.IsSuccess)
            return HandleFailure(result);

        return Ok(new { user = result.Value });
    }
}
