

using Application.Common.Interfaces;
using AutoMapper;
using Chatserver.Application.Common.Models;
using ChatServer.Domain.Events;
using ChatSrever.Domain.Entities;
using Domain.Events;
using Domain.Events.RoomEvent;

namespace Application.Authentication.Commands;

public class CreateMessagesRoomCommand : IRequestWrapper<RoomMessages>
{
    public int RoomId { get; set; }

    public int MessagesId { get; set; }
}

public class CreateMessagesRoomCommandHandler : IRequestHandlerWrapper<CreateMessagesRoomCommand, RoomMessages>
{
    private readonly IApplicationDbContext _context;




    public CreateMessagesRoomCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<RoomMessages>> Handle(CreateMessagesRoomCommand request, CancellationToken cancellationToken)
    {
        var roomMessages = new RoomMessages()
        {
            MessagesId = request.MessagesId,
            RoomId = request.RoomId,
        };

        roomMessages.AddDomainEvent(new CreatedEvent<RoomMessages>(roomMessages));

         _context.RoomMessages.Add(roomMessages);

        await _context.SaveChangesAsync(cancellationToken);


        return ServiceResult.Success(roomMessages);
    } 
}
