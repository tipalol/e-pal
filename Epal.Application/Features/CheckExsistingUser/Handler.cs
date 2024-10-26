using Epal.Application.Features.CheckExsistingUser.Models;
using Epal.Application.Interfaces;
using Epal.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.CheckExsistingUser;

public record CheckUserRequest(string Email) : IRequest<StatusResponse>;

internal sealed class Handler(IEpalDbContext context) : IRequestHandler<CheckUserRequest, StatusResponse>
{
    public async Task<StatusResponse> Handle(CheckUserRequest request, CancellationToken cancellationToken)
    {
        var exists = await context.Users
            .Where(x => x.Email == request.Email)
            .Select(x => new StatusResponse(true, x.Status))
            .SingleOrDefaultAsync(cancellationToken);

        return exists ?? new StatusResponse(false);
    }
}