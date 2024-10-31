using Epal.Application.Common;
using Epal.Application.Features.Services.GetCategories.Models;
using Epal.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Epal.Application.Features.Services.GetCategories;

public record GetCategoriesRequest(Guid ProfileId) : IRequest<Result<IEnumerable<CategoryListView>>>;

internal sealed class Handler(IEpalDbContext context) : IRequestHandler<GetCategoriesRequest, Result<IEnumerable<CategoryListView>>>
{
    public async Task<Result<IEnumerable<CategoryListView>>> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
    {
        var profile = await context.Users
            .Include(x => x.Services)
            .ThenInclude(x => x.Category)
            .SingleOrDefaultAsync(x => x.Id == request.ProfileId, cancellationToken);

        if (profile == null)
            return Result<IEnumerable<CategoryListView>>.Fail("User not found");

        var serviceTypes = profile.Services
            .Select(x => new CategoryListView(x.Category.Id, x.Category.Name, x.Category.Avatar))
            .DistinctBy(x => x.Id)
            .ToArray();

        return Result<IEnumerable<CategoryListView>>.Ok(serviceTypes);
    }
}
