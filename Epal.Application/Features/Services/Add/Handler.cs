using Epal.Application.Common;
using Epal.Application.Features.Services.Add.Models;
using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Services.Add;

public record CreateServiceRequest(ServiceDto ServiceDto) : IRequest<Result>;

internal sealed class Handler(IEpalDbContext context, IUserService userService) : IRequestHandler<CreateServiceRequest, Result>
{
    public async Task<Result> Handle(CreateServiceRequest request, CancellationToken cancellationToken)
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
