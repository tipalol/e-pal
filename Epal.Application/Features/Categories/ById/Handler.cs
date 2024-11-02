using Epal.Application.Common;
using Epal.Application.Features.Categories.Models;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Categories.ById;

public record GetCategoryById(Guid ServiceTypeId) : IRequest<Result<CategoryListView>>;

public class Handler(IEpalDbContext context) : IRequestHandler<GetCategoryById, Result<CategoryListView>>
{
    public async Task<Result<CategoryListView>> Handle(GetCategoryById request, CancellationToken cancellationToken)
    {
        var serviceType = await context.Categories
            .SingleOrDefaultAsync(x => x.Id == request.ServiceTypeId, cancellationToken);

        if (serviceType == null)
            return Result<CategoryListView>.Fail("Service type not found");

        return Result<CategoryListView>.Ok(new CategoryListView(serviceType.Id, serviceType.Name, serviceType.Avatar));
    }
}
