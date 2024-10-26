using Epal.Application.Features.EMailConfirmation;
using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using Epal.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.CheckExsistingUser;

public record CheckUserRequest(string Email) : IRequest<UserStatus>;

internal sealed class Handler(IEpalDbContext context) : IRequestHandler<CheckUserRequest, UserStatus>
{
    public async Task<UserStatus> Handle(CheckUserRequest request, CancellationToken cancellationToken)
    {
        var result = UserStatus.None;
        var user = await context.Users.FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
        if (user is not null)
            result = user.Status;
        return await Task.FromResult(result);
    }
}