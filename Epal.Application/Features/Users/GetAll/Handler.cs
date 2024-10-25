using Epal.Domain.Entities;
using MediatR;

namespace Epal.Application.Features.Users.GetAll;

public record GetUsersRequest : IRequest<IEnumerable<User>>;

internal sealed class Handler : IRequestHandler<GetUsersRequest, IEnumerable<User>>
{
    public Task<IEnumerable<User>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IEnumerable<User>>(new List<User>());
    }
}