using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Users.GetAll;

public record GetUsersRequest : IRequest<IEnumerable<User>>;

internal sealed class Handler(IEpalDbContext context) : IRequestHandler<GetUsersRequest, IEnumerable<User>>
{
    public async Task<IEnumerable<User>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        return await context.Users.ToListAsync(cancellationToken);
    }
}