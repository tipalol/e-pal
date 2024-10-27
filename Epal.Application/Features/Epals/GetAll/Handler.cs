using Epal.Application.Common;
using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Epals.GetAll;

public record GetEpalsRequest : IRequest<Result<IEnumerable<EpalProfile>>>;

internal sealed class Handler(IEpalDbContext context) : IRequestHandler<GetEpalsRequest, Result<IEnumerable<EpalProfile>>>
{
    public async Task<Result<IEnumerable<EpalProfile>>> Handle(GetEpalsRequest request, CancellationToken cancellationToken)
    {
        var epals = await context.Users.Where(x => x.GetType() == typeof(EpalProfile)).Select(x=>(EpalProfile)x).ToListAsync(cancellationToken);
        if (!epals.Any())
            return Result<IEnumerable<EpalProfile>>.Fail("Epals not found");
        return Result<IEnumerable<EpalProfile>>.Ok(epals.AsEnumerable());
    }
}