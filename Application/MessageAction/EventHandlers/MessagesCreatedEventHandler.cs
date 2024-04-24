
using ChatSrever.Domain.Entities;
using Domain.Events;
using Microsoft.Extensions.Logging;

namespace ChatServer.Application.TodoItems.EventHandlers;

public class MessagesRoomCreatedEventHandler : INotificationHandler<CreatedEvent<Messages>>
{
    private readonly ILogger<MessagesRoomCreatedEventHandler> _logger;

    public MessagesRoomCreatedEventHandler(ILogger<MessagesRoomCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CreatedEvent<Messages> notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
