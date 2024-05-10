

using Application.Common.Interfaces;
using Application.Dot;
using AutoMapper;
using Chatserver.Application.Common.Models;
using ChatServer.Application.TodoItems.EventHandlers;
using ChatServer.Domain.Events;
using ChatSrever.Domain.Entities;
using Domain.Events;
using Domain.Events.RoomEvent;
using System.Collections.Generic;


namespace Application.Authentication.Commands;

public record CreateRoomTrueCommand : IRequestWrapper<RoomDot>
{
    public string? latestMessage { get; set; }

    public int QuantityUser { get; set; }
    public bool type { get; set; }

    public List<int> ListId { get; set; }
}

public class CreateRoomCommandHandler : IRequestHandlerWrapper<CreateRoomTrueCommand, RoomDot>
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;
    private readonly IMapper mapper;


    public CreateRoomCommandHandler(IApplicationDbContext context, IMediator mediator, IMapper mapper)
    {
        _context = context;
        _mediator = mediator;
        this.mapper = mapper;
    }

    public async Task<ServiceResult<RoomDot>> Handle(CreateRoomTrueCommand request, CancellationToken cancellationToken)
    {
        var rooms = new Rooms()
        {
            Title = request.latestMessage,
            QuantityUser = request.QuantityUser,
            IsGroup = request.type,
        };
        
   //     rooms.AddDomainEvent(new RoomCreatedEvent(rooms));

        await _context.Rooms.AddAsync(rooms);

        await _context.SaveChangesAsync(cancellationToken);

        var roomId = rooms.Id;
        foreach (var item in request.ListId)
        {
            await _mediator.Publish(new CreatedEvent<RoomUser>(new RoomUser { UserId = item, RoomId = roomId }));
        }


      

        return ServiceResult.Success(mapper.Map<RoomDot>(rooms));
    } 
}
