using Epal.Domain.Entities;
using MediatR;

namespace Epal.Application.Features.Users.Get;

public record GetUserRequest(Guid Id) : IRequest<User>;

internal sealed class Handler : IRequestHandler<GetUserRequest, User>
{
    public Task<User> Handle(GetUserRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new User()
        {
            Email = "123@123.ru",
            Password = "12352452345"
        });
    }
}