using Epal.Application.Common;
using Epal.Application.Features.Services.Add.Models;
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
        var profile = await context.Users
            .SingleOrDefaultAsync(x => x.Id == profileId, cancellationToken);

        if (profile == null)
            return Result.Fail("User not found");

        var serviceDto = request.ServiceDto;
        var serviceType = await context.ServiceTypes
            .SingleOrDefaultAsync(x => x.Id == serviceDto.ServiceTypeId, cancellationToken);

        if (serviceType == null)
            return Result.Fail("Service type not found");

        if (serviceDto.Id.HasValue)
        {
            var dbService = await context.Services
                .SingleOrDefaultAsync(x => x.Id == serviceDto.Id.Value, cancellationToken);

            if (dbService == null)
                return Result.Fail("Service not found");

            dbService.Name = serviceDto.Name;
            dbService.Description = serviceDto.Description;
            dbService.Price = serviceDto.Price;
            dbService.ServiceTypeId = serviceDto.ServiceTypeId;
            dbService.Avatar = serviceDto.Avatar;

            await context.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }

        var service = new Service
        {
            Name = serviceDto.Name,
            Description = serviceDto.Description,
            Avatar = serviceDto.Avatar,
            ServiceTypeId = serviceType.Id,
            ProfileId = profile.Id,
            Price = serviceDto.Price
        };

        profile.Services.Add(service);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
