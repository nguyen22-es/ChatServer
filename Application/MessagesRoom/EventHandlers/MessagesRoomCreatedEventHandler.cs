
using ChatSrever.Domain.Entities;
using Domain.Events;
using Domain.Events.RoomEvent;
using Microsoft.Extensions.Logging;

namespace ChatServer.Application.EventHandlers;

public class MessagesRoomCreatedEventHandler : INotificationHandler<MessagesRoomCreatedEvent>
{
    private readonly ILogger<MessagesRoomCreatedEventHandler> _logger;

    public MessagesRoomCreatedEventHandler(ILogger<MessagesRoomCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(MessagesRoomCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
