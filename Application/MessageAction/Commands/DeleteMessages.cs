
using Application.Common.Interfaces;
using Chatserver.Application.Common.Models;
using ChatServer.Domain.Events;
using ChatSrever.Domain.Entities;
using MediatR.Wrappers;

namespace Application.Authentication.Commands;

public record DeleteMessagesCommand(int Id) : IRequestWrapper<Messages>;

public class DeleteMessagesCommandHandler : IRequestHandlerWrapper<DeleteMessagesCommand, Messages>
{
    private readonly IApplicationDbContext _context;

    public DeleteMessagesCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<Messages>> Handle(DeleteMessagesCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Messages
              .Where(l => l.Id == request.Id)
              .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Messages), request.Id.ToString());
        }

        _context.Messages.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(entity);
    }

}
