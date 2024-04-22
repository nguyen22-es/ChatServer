
using Application.Common.Interfaces;
using ChatServer.Domain.Events;

namespace Application.Authentication.Commands;

public record DeleteUserCommand(int Id) : IRequest;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Users
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Users.Remove(entity);

        entity.AddDomainEvent(new UserDeletedEvent(entity));

        await _context.SaveChangesAsync(cancellationToken);
    }

}
