

using Application.Common.Interfaces;
using AutoMapper;
using Chatserver.Application.Common.Models;
using ChatServer.Application.TodoItems.EventHandlers;
using ChatServer.Domain.Events;
using ChatSrever.Domain.Entities;
using Domain.Events;
using Domain.Events.RoomEvent;
using System.Collections.Generic;


namespace Application.Authentication.Commands;

public record CreateRoomTrueCommand : IRequestWrapper<Rooms>
{
    public string? Title { get; set; }

    public int QuantityUser { get; set; }
    public bool type { get; set; }

    public List<int> ListId { get; set; }
}

public class CreateRoomCommandHandler : IRequestHandlerWrapper<CreateRoomTrueCommand, Rooms>
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;



    public CreateRoomCommandHandler(IApplicationDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<ServiceResult<Rooms>> Handle(CreateRoomTrueCommand request, CancellationToken cancellationToken)
    {
        var rooms = new Rooms()
        {
            Title = request.Title,
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
      



        return ServiceResult.Success(rooms);
    } 
}
