

using Application.Common.Interfaces;
using AutoMapper;
using Chatserver.Application.Common.Models;
using ChatServer.Domain.Events;
using ChatSrever.Domain.Entities;
using Domain.Events;
using Domain.Events.RoomEvent;

namespace Application.Authentication.Commands;

public record CreateRoomUserCommand : IRequestWrapper<RoomUser>
{
    public int UserId { get; set; }

    public int RoomId { get; set; }
}

public class CreateRoomUserCommandHandler : IRequestHandlerWrapper<CreateRoomUserCommand, RoomUser>
{
    private readonly IApplicationDbContext _context;




    public CreateRoomUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<RoomUser>> Handle(CreateRoomUserCommand request, CancellationToken cancellationToken)
    {
        var roomUser = new RoomUser()
        {
            RoomId = request.RoomId,
            UserId = request.UserId,
        };

        roomUser.AddDomainEvent(new CreatedEvent<RoomUser>(roomUser));

        await _context.RoomUsers.AddAsync(roomUser);

        await _context.SaveChangesAsync(cancellationToken);


        return ServiceResult.Success(roomUser);
    } 
}
