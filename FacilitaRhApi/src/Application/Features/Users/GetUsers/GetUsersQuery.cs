using FacilitaRhApi.Application.Abstractions.Messaging;

namespace FacilitaRhApi.Application.Features.Users.GetUsers;

public record GetUsersQuery() : IQuery<IEnumerable<UserResponse>>;
