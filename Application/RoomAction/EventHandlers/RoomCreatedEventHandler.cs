
using Application.Common.Interfaces;
using ChatSrever.Domain.Entities;
using Domain.Events;
using Microsoft.Extensions.Logging;

namespace ChatServer.Application.TodoItems.EventHandlers;

public class RoomCreatedEventHandler : INotificationHandler<CreatedEvent<RoomUser>>
{
    private readonly ILogger<RoomCreatedEventHandler> _logger;
    private readonly IApplicationDbContext _context;


    public RoomCreatedEventHandler(ILogger<RoomCreatedEventHandler> logger, IApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task Handle(CreatedEvent<RoomUser> notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Phòng được tạo: Người dùng '{notification.Item.UserId}' đã tham gia phòng '{notification.Item.RoomId}'");


        await _context.RoomUsers.AddAsync(new RoomUser { RoomId = notification.Item.RoomId,UserId = notification.Item.UserId});



        await _context.SaveChangesAsync(cancellationToken);


     

    }
}
