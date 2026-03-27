using Microsoft.AspNetCore.Identity;
using FacilitaRhApi.Application.Abstractions.Messaging;
using FacilitaRhApi.Domain.Abstractions;
using FacilitaRhApi.Domain.Models;

namespace FacilitaRhApi.Application.Features.Users.CreateUser;

public sealed class CreateUserCommandHandler : ICommandHandler<CreateUserCommand, string>
{
    private readonly UserManager<User> _userManager;

    public CreateUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<string>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser is not null)
            return UserErrors.EmailAlreadyRegistered;

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            UserName = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
            return UserErrors.CreationFailed;

        return user.Id;
    }
}
