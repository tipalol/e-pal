using Epal.Application.Common;
using Epal.Application.Features.Profiles.Models;
using Epal.Application.Features.Profiles.Post;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto;

namespace Epal.Application.Features.Activity.Post;

public record CreateActivityRequest(string Name, string Description, string Avatar, string Icon) : IRequest<Result>;

internal sealed class Handler(IEpalDbContext context, IUserService userService) : IRequestHandler<CreateActivityRequest, Result>
{
    public async Task<Result> Handle(CreateActivityRequest request, CancellationToken cancellationToken)
    {
        try
        {
            Guid newActivityId = Guid.NewGuid();
            while (await context.Services.AnyAsync(x=>x.Id == newActivityId, cancellationToken))
            {
                newActivityId = Guid.NewGuid();
            }

            await context.Services.AddAsync(new Domain.Entities.Service()
            {
                Avatar = request.Avatar,
                Description = request.Description,
                Icon = request.Icon,
                Name = request.Name
            }, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            return Result.Fail(e.Message);
        }
        return Result.Ok();
    }
}
