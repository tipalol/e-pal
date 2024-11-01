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

        var serviceOptionDto = request.ServiceOptionDto;
        var service = await context.Services
            .SingleOrDefaultAsync(x => x.Id == serviceOptionDto.ServiceId, cancellationToken);

        if (service == null)
            return Result.Fail("Service not found");

        if (serviceOptionDto.Id.HasValue)
        {
            var dbServiceOption = await context.ServiceOptions
                .SingleOrDefaultAsync(x => x.Id == serviceOptionDto.Id.Value, cancellationToken);

            if (dbServiceOption == null)
                return Result.Fail("Service option not found");

            dbServiceOption.Name = serviceOptionDto.Name;
            dbServiceOption.Price = serviceOptionDto.Price;
            dbServiceOption.ServiceId = serviceOptionDto.ServiceId;
            dbServiceOption.Description = serviceOptionDto.Description;

            await context.SaveChangesAsync(cancellationToken);

            return Result.Ok();
        }

        var serviceOption = new ServiceOption
        {
            Name = serviceOptionDto.Name,
            ServiceId = service.Id,
            Price = serviceOptionDto.Price,
            Description = serviceOptionDto.Description
        };

        await context.ServiceOptions.AddAsync(serviceOption, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
