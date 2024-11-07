using Epal.Application.Common;
using Epal.Application.Features.Services.AddOrUpdate;
using Epal.Application.Features.Services.ExtraInfo.AddOrUpdate.Models;
using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Services.ExtraInfo.AddOrUpdate;

public record AddOrUpdateServicesExtraInfoRequest(ServiceExtraInfoDto ServiceExtraInfoDto) : IRequest<Result>;

internal sealed class Handler(IEpalDbContext context, IUserService userService)
    : IRequestHandler<AddOrUpdateServicesExtraInfoRequest, Result>
{
    public async Task<Result> Handle(AddOrUpdateServicesExtraInfoRequest request, CancellationToken cancellationToken)
    {
        var profileId = userService.AuthenticatedUser?.Id;
        if (profileId is null)
            return Result.Fail("Not authenticated user");

        var profile = await context.Profiles
            .SingleOrDefaultAsync(x => x.Id == profileId, cancellationToken);
        if (profile == null)
            return Result.Fail("User not found");

        var ServiceExtraInfoDto = request.ServiceExtraInfoDto;
        var service = await context.Services
            .SingleOrDefaultAsync(x => x.Id == ServiceExtraInfoDto.ServiceId, cancellationToken);
        if (service is null)
            return Result.Fail("Service not found");
        if (service.ProfileId != profileId)
            return Result.Fail("It's not your service, you cant edit that");


        var dbSEI = await context.ServiceExtraInfos.SingleOrDefaultAsync(
            x => x.ServiceId == ServiceExtraInfoDto.ServiceId, cancellationToken);
        if (dbSEI is not null)
        {
            dbSEI.Rank = ServiceExtraInfoDto.Rank;
            dbSEI.Servers = ServiceExtraInfoDto.Servers;
            dbSEI.Styles = ServiceExtraInfoDto.Styles;
            dbSEI.Platforms = ServiceExtraInfoDto.Platforms;
            dbSEI.Positions = ServiceExtraInfoDto.Positions;
            dbSEI.Photo = ServiceExtraInfoDto.Photo;
            await context.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }

        var SEI = new ServiceExtraInfo()
        {
            ServiceId = ServiceExtraInfoDto.ServiceId,
            Rank = ServiceExtraInfoDto.Rank,
            Servers = ServiceExtraInfoDto.Servers,
            Styles = ServiceExtraInfoDto.Styles,
            Platforms = ServiceExtraInfoDto.Platforms,
            Positions = ServiceExtraInfoDto.Positions,
            Photo = ServiceExtraInfoDto.Photo,
        };
        await context.ServiceExtraInfos.AddAsync(SEI, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return Result.Ok();
    }
}