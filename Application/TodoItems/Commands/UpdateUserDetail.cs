

using ChatServer.Application.Common.Interfaces;

namespace Application.TodoItems.Commands;

public record UpdateTodoItemDetailCommand : IRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }

    public string? account { get; set; }

    public string? Password { get; set; }
}

public class UpdateTodoItemDetailCommandHandler : IRequestHandler<UpdateTodoItemDetailCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemDetailCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Users
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;
        entity.account = request.account;
        entity.Password = request.Password;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
