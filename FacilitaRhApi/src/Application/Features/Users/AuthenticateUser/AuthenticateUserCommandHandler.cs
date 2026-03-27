using Microsoft.AspNetCore.Identity;
using FacilitaRhApi.Application.Abstractions;
using FacilitaRhApi.Application.Abstractions.Messaging;
using FacilitaRhApi.Domain.Abstractions;
using FacilitaRhApi.Domain.Models;

namespace FacilitaRhApi.Application.Features.Users.AuthenticateUser;

public sealed class AuthenticateUserCommandHandler : ICommandHandler<AuthenticateUserCommand, AuthenticateUserResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthenticateUserCommandHandler(
        UserManager<User> userManager,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Result<AuthenticateUserResponse>> Handle(
        AuthenticateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return UserErrors.NotFound;

        var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!passwordValid)
            return UserErrors.InvalidPassword;

        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email!);

        return new AuthenticateUserResponse(user.Id, user.Email!, token);
    }
}
