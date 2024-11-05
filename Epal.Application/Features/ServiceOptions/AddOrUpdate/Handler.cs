using Epal.Application.Common;
using Epal.Application.Features.ServiceOptions.AddOrUpdate.Models;
using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.ServiceOptions.AddOrUpdate;

public record CreateOrUpdateServiceOptionRequest(ServiceOptionDto ServiceOptionDto) : IRequest<Result>;

internal sealed class Handler(IEpalDbContext context, IUserService userService) : IRequestHandler<CreateOrUpdateServiceOptionRequest, Result>
{
    public async Task<Result> Handle(CreateOrUpdateServiceOptionRequest request, CancellationToken cancellationToken)
    {
        var profileId = userService.AuthenticatedUser.Id;
        var profile = await context.Profiles
            .SingleOrDefaultAsync(x => x.Id == profileId, cancellationToken);

        if (profile == null)
            return Result.Fail("User not found");

        var serviceDto = request.ServiceOptionDto;
        var service = await context.Services
            .SingleOrDefaultAsync(x => x.Id == serviceDto.ServiceId, cancellationToken);

        if (service == null)
            return Result.Fail("Service not found");

        if (serviceDto.Id.HasValue)
        {
            var dbServiceOption = await context.ServiceOptions
                .SingleOrDefaultAsync(x => x.Id == serviceDto.Id.Value, cancellationToken);

            if (dbServiceOption == null)
                return Result.Fail("Service option not found");

            dbServiceOption.Name = serviceDto.Name;
            dbServiceOption.Price = serviceDto.Price;
            dbServiceOption.ServiceId = serviceDto.ServiceId;
            dbServiceOption.Description = serviceDto.Description;

            await context.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }

        var serviceOption = new ServiceOption
        {
            Name = serviceDto.Name,
            ServiceId = service.Id,
            Price = serviceDto.Price,
            Description = serviceDto.Description
        };

        await context.ServiceOptions.AddAsync(serviceOption, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
