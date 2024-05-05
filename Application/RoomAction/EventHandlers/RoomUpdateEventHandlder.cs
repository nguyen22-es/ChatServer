
using Application.Common.Interfaces;
using ChatSrever.Domain.Entities;
using Domain.Events;
using Domain.Events.RoomEvent;
using Mapster;
using Microsoft.Extensions.Logging;

namespace ChatServer.Application.TodoItems.EventHandlers;

public class RoomUpdateEventHandlder : INotificationHandler<RoomCompletedEvent>
{
    private readonly ILogger<RoomUpdateEventHandlder> _logger;
    private readonly IApplicationDbContext _context;


    public RoomUpdateEventHandlder(ILogger<RoomUpdateEventHandlder> logger, IApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Handle(RoomCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation(notification.ToString());

        var room = await _context.Rooms.FindAsync(notification.Room.Id);

        room.Title = notification.Room.Title;

         _context.Rooms.Update(room);

        await _context.SaveChangesAsync(cancellationToken);




    }
}
