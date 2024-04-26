
using Application.Common.Interfaces;
using Chatserver.Application.Common.Models;
using ChatServer.Application.ApplicationUser.Queries.GetToken;
using ChatServer.Domain.Events;
using ChatSrever.Domain.Entities;
using MediatR.Wrappers;

namespace Application.Authentication.Commands;

public record DeleteRoomCommand(int Id) : IRequestWrapper<Rooms>;

public class DeleteRoomCommandHandler : IRequestHandlerWrapper<DeleteRoomCommand, Rooms>
{
    private readonly IApplicationDbContext _context;

    public DeleteRoomCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<Rooms>> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Rooms
              .Where(l => l.Id == request.Id)
              .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            return ServiceResult.Failed<Rooms>(ServiceError.NotFound);
        }

        _context.Rooms.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(entity);
    }

  
}
