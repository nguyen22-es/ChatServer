
using ChatSrever.Domain.Entities;
using Domain.Events;
using Microsoft.Extensions.Logging;

namespace Chatserver.Application.TodoItems.EventHandlers;

/*public class MessagesCompletedEventHandler : INotificationHandler<CompletedEvent>
{
    private readonly ILogger<CompletedEvent> _logger;

    public MessagesCompletedEventHandler(ILogger<CompletedEvent> logger)
    {
        _logger = logger;
    }

    public Task Handle(CompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("CleanArchitecture Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}*/
