using Epal.Application.Features.Status.Models;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Status;

public record StatusUserRequest(string Email) : IRequest<StatusResponse>;

internal sealed class Handler(IEpalDbContext context) : IRequestHandler<StatusUserRequest, StatusResponse>
{
    public async Task<StatusResponse> Handle(StatusUserRequest request, CancellationToken cancellationToken)
    {
        var exists = await context.Profiles
            .Where(x => x.Email == request.Email)
            .Select(x => new StatusResponse(true, x.Status))
            .SingleOrDefaultAsync(cancellationToken);

        return exists ?? new StatusResponse(false);
    }
}