using ChatServer.Application.Common.Interfaces;
using ChatServer.Domain.Events;
using ChatSrever.Domain.Entities;

namespace Application.TodoItems.Commands;

public record CreateUserCommand : IRequest<int>
{
    public string? Name { get; set; }

    public string? account { get; set; }

    public string? Password { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = new User
        {
            Name = request.Name,
            account = request.account,
            Password = request.Password
        };

        entity.AddDomainEvent(new UserCreatedEvent(entity));

        _context.Users.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
