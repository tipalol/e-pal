using Epal.Application.Common;
using Epal.Application.Features.Services.GetCategories.Models;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Services.ById;

public record GetServiceTypeById(Guid ServiceTypeId) : IRequest<Result<CategoryListView>>;

public class Handler(IEpalDbContext context) : IRequestHandler<GetServiceTypeById, Result<CategoryListView>>
{
    public async Task<Result<CategoryListView>> Handle(GetServiceTypeById request, CancellationToken cancellationToken)
    {
        var serviceType = await context.Categories
            .SingleOrDefaultAsync(x => x.Id == request.ServiceTypeId, cancellationToken);

        if (serviceType == null)
            return Result<CategoryListView>.Fail("Service type not found");
        
        return Result<CategoryListView>.Ok(new CategoryListView(serviceType.Id, serviceType.Name, serviceType.Avatar));
    }
}