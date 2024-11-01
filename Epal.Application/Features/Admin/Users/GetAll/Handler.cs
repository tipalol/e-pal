using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Admin.Users.GetAll;

public record GetUsersRequest : IRequest<IEnumerable<Profile>>;

internal sealed class Handler(IEpalDbContext context) : IRequestHandler<GetUsersRequest, IEnumerable<Profile>>
{
    public async Task<IEnumerable<Domain.Entities.Profile>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        return await context.Profiles.ToListAsync(cancellationToken);
    }
}