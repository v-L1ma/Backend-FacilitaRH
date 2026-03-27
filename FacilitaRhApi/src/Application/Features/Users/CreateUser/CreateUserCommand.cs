using FacilitaRhApi.Application.Abstractions.Messaging;

namespace FacilitaRhApi.Application.Features.Users.CreateUser;

public record CreateUserCommand(string Name, string Email, string Password) : ICommand<string>;
