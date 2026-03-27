using FacilitaRhApi.Application.Abstractions.Messaging;

namespace FacilitaRhApi.Application.Features.Users.AuthenticateUser;

public record AuthenticateUserCommand(string Email, string Password) : ICommand<AuthenticateUserResponse>;
