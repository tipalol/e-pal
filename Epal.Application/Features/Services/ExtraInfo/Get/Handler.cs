using Epal.Application.Common;
using Epal.Application.Features.Services.ExtraInfo.Get.Model;
using Epal.Application.Features.Services.Get;
using Epal.Application.Features.Services.Get.Models;
using Epal.Application.Interfaces;
using Epal.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Services.ExtraInfo.Get;
using MediatR;


public record GetServicesExtraInfoRequest(Guid ServiceId) : IRequest<Result<ServiceExtraInfoView>>;

internal sealed class Handler(IEpalDbContext context) : IRequestHandler<GetServicesExtraInfoRequest, Result<ServiceExtraInfoView>>
{
    public async Task<Result<ServiceExtraInfoView>> Handle(GetServicesExtraInfoRequest request, CancellationToken cancellationToken)
    {
        var dbServiceExtraInfo =
            await context.ServiceExtraInfos.SingleOrDefaultAsync(x => x.ServiceId == request.ServiceId,
                cancellationToken);
        return Result<ServiceExtraInfoView>.Ok(dbServiceExtraInfo is null ? new ServiceExtraInfoView() : new ServiceExtraInfoView(dbServiceExtraInfo));
    }
}

