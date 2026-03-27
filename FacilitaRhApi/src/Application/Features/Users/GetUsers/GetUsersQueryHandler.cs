using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FacilitaRhApi.Application.Abstractions.Messaging;
using FacilitaRhApi.Domain.Abstractions;
using FacilitaRhApi.Domain.Models;

namespace FacilitaRhApi.Application.Features.Users.GetUsers;

public sealed class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, IEnumerable<UserResponse>>
{
    private readonly UserManager<User> _userManager;

    public GetUsersQueryHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<IEnumerable<UserResponse>>> Handle(
        GetUsersQuery request,
        CancellationToken cancellationToken)
    {
        var users = await _userManager.Users
            .Select(u => new UserResponse(u.Id, u.Name, u.Email!))
            .ToListAsync(cancellationToken);

        return users;
    }
}
