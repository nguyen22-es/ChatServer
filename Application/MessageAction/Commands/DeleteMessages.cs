
using Application.Common.Interfaces;
using Chatserver.Application.Common.Models;
using ChatServer.Domain.Events;
using ChatSrever.Domain.Entities;
using MediatR.Wrappers;
using System;
using System.Data.SqlTypes;

namespace Application.Authentication.Commands;

public record DeleteMessagesCommand(int Id) : IRequestWrapper<INullable>;

public class DeleteMessagesCommandHandler : IRequestHandlerWrapper<DeleteMessagesCommand, INullable>
{
    private readonly IApplicationDbContext _context;

    public DeleteMessagesCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<INullable>> Handle(DeleteMessagesCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.RoomMessages
              .Where(l => l.MessagesId == request.Id)
              .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            return ServiceResult.Failed<INullable>(ServiceError.NotFound);
        }

        _context.RoomMessages.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
        
        INullable nullable = entity as INullable;
       
        return ServiceResult.Success(nullable);
    }

}
