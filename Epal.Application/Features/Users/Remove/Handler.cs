using MediatR;

namespace Epal.Application.Features.Users.Remove;

public record RemoveUserRequest(Guid Id) : IRequest;

internal sealed class Handler : IRequestHandler<RemoveUserRequest>
{
    public Task Handle(RemoveUserRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}