

using Application.Common.Interfaces;
using AutoMapper;
using Chatserver.Application.Common.Models;
using ChatServer.Domain.Events;
using ChatSrever.Domain.Entities;
using Domain.Events;
using Domain.Events.MessagesRoom;
using Domain.Events.RoomEvent;

namespace Application.Authentication.Commands;

public class CreateMessagesRoomCommand : INotification
{
    public int RoomId { get; set; }

    public int MessagesId { get; set; }
}

public class CreateMessagesRoomCommandHandler : INotificationHandler<MessagesRoomCreatedEvent>
{
    private readonly IApplicationDbContext _context;




    public CreateMessagesRoomCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(MessagesRoomCreatedEvent request, CancellationToken cancellationToken)
    {
        var roomMessages = new RoomMessages()
        {
            MessagesId = request.Room.MessagesId,
            RoomId = request.Room.RoomId,
        };

       

        await _context.RoomMessages.AddAsync(roomMessages);

        await _context.SaveChangesAsync(cancellationToken);


       
    } 
}
