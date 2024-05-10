

using Application.Common.Interfaces;
using AutoMapper;
using Chatserver.Application.Common.Models;
using ChatServer.Domain.Events;
using ChatSrever.Domain.Entities;
using ChatServer.Application.EventHandlers;
using Domain.Events;
using Domain.Events.MessagesRoom;
using Microsoft.Extensions.Logging;
using Domain.Events.RoomEvent;
using Application.Dot;

namespace Application.Authentication.Commands;

public record CreateMessagesCommand : IRequestWrapper<MessagesDot>
{
    public int RoomId { get; set; }

    public string content { get; set; }

    public string nameSend { get; set; }

    public int UserId { get; set; }
}

public class CreateMessagesCommandHandler : IRequestHandlerWrapper<CreateMessagesCommand, MessagesDot>
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CreateMessagesCommandHandler(IApplicationDbContext context, IMediator mediator, IMapper mapper)
    {
        _context = context;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<ServiceResult<MessagesDot>> Handle(CreateMessagesCommand request, CancellationToken cancellationToken)
    {
        var message = new Messages()
        {
            Content = request.content,
            UserId = request.UserId,


        };

        message.AddDomainEvent(new CreatedEvent<Messages>(message));

        var m = await _context.Messages.AddAsync(message);

        await _context.SaveChangesAsync(cancellationToken);

        await _mediator.Publish(new MessagesRoomCreatedEvent(new RoomMessages { MessagesId = message.Id, RoomId = request.RoomId }));
        await _mediator.Publish(new RoomCompletedEvent(new Rooms { Title = message.Content, Id = request.RoomId }));

        var MessageDot = _mapper.Map<MessagesDot>(m.Entity);
        MessageDot.nameSend = request.nameSend;

        return ServiceResult.Success(MessageDot);
    }
}
