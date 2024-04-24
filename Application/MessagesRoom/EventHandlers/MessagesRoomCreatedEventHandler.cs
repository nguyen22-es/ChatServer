
using ChatSrever.Domain.Entities;
using Domain.Events;
using Microsoft.Extensions.Logging;

namespace ChatServer.Application.EventHandlers;

public class MessagesRoomCreatedEventHandler : INotificationHandler<CreatedEvent<RoomMessages>>
{
    private readonly ILogger<MessagesRoomCreatedEventHandler> _logger;

    public MessagesRoomCreatedEventHandler(ILogger<MessagesRoomCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CreatedEvent<RoomMessages> notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
