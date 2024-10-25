using Epal.Domain.Entities;
using MediatR;

namespace Epal.Application.Features.Users.Add;

public record CreateUserRequest(Guid Id) : IRequest<User>;

internal sealed class Handler : IRequestHandler<CreateUserRequest, User>
{
    public Task<User> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}