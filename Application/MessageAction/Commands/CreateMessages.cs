

using Application.Common.Interfaces;
using AutoMapper;
using Chatserver.Application.Common.Models;
using ChatServer.Domain.Events;
using ChatSrever.Domain.Entities;
using ChatServer.Application.EventHandlers;
using Domain.Events;
using Domain.Events.MessagesRoom;
using Microsoft.Extensions.Logging;

namespace Application.Authentication.Commands;

public record CreateMessagesCommand : IRequestWrapper<Messages>
{
    public  int RoomMessageId {  get; set; }

    public string content { get; set; }

    public int UserId { get; set; }
}

public class CreateMessagesCommandHandler : IRequestHandlerWrapper<CreateMessagesCommand, Messages>
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;

    public CreateMessagesCommandHandler(IApplicationDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
      
    }

    public async Task<ServiceResult<Messages>> Handle(CreateMessagesCommand request, CancellationToken cancellationToken)
    {
        var message = new Messages()
        {
            Content = request.content,
            UserId = request.UserId,
            RoomMessageId = request.RoomMessageId
        };

        message.AddDomainEvent(new CreatedEvent<Messages>(message));

        await _context.Messages.AddAsync(message);

        await _context.SaveChangesAsync(cancellationToken);

        await _mediator.Publish(new CreatedEvent<RoomMessages>(new RoomMessages { MessagesId = message.Id,RoomId = request.RoomMessageId }));


        return ServiceResult.Success(message);
    } 
}
