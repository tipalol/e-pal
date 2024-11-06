using Epal.Application.Common;
using Epal.Application.Features.ServiceOptions.AddOrUpdate.Models;
using Epal.Application.Features.Services.AddOrUpdate.Models;
using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Services.AddOrUpdate;

public record CreateOrUpdateServiceRequest(ServiceDto ServiceDto) : IRequest<Result>;

internal sealed class Handler(IEpalDbContext context, IUserService userService) : IRequestHandler<CreateOrUpdateServiceRequest, Result>
{
    public async Task<Result> Handle(CreateOrUpdateServiceRequest request, CancellationToken cancellationToken)
    {
        var profileId = userService.AuthenticatedUser.Id;
        var profile = await context.Profiles
            .SingleOrDefaultAsync(x => x.Id == profileId, cancellationToken);

        if (profile == null)
            return Result.Fail("User not found");

        var serviceDto = request.ServiceDto;
        var category = await context.Categories
            .SingleOrDefaultAsync(x => x.Id == serviceDto.CategoryId, cancellationToken);

        if (category == null)
            return Result.Fail("Category not found");

        if (serviceDto.Id.HasValue)
        {
            var dbService = await context.Services
                .SingleOrDefaultAsync(x => x.Id == serviceDto.Id.Value, cancellationToken);

            if (dbService == null)
                return Result.Fail("Service not found");

            dbService.Name = serviceDto.Name;
            dbService.Description = serviceDto.Description;
            dbService.CategoryId = serviceDto.CategoryId;
            dbService.Tags = serviceDto.Tags;

            await context.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }

        var service = new Service
        {
            Name = serviceDto.Name,
            CategoryId = category.Id,
            Icon = category.Avatar+"?width=100&height=100",
            Description = serviceDto.Description,
            Tags = serviceDto.Tags,
            ProfileId = profileId
        };

        await context.Services.AddAsync(service, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
