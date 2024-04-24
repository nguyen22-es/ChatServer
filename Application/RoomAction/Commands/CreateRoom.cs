

using Application.Common.Interfaces;
using AutoMapper;
using Chatserver.Application.Common.Models;
using ChatServer.Domain.Events;
using ChatSrever.Domain.Entities;
using Domain.Events.RoomEvent;


namespace Application.Authentication.Commands;

public record CreateRoomCommand : IRequestWrapper<Rooms>
{
    public string? Title { get; set; }

    public int QuantityUser { get; set; }
}

public class CreateRoomCommandHandler : IRequestHandlerWrapper<CreateRoomCommand, Rooms>
{
    private readonly IApplicationDbContext _context;




    public CreateRoomCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
    }

    public async Task<ServiceResult<Rooms>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
    {
        var rooms = new Rooms()
        {
            Title = request.Title,
            QuantityUser = request.QuantityUser,
        };

        rooms.AddDomainEvent(new RoomCreatedEvent(rooms));

        await _context.Rooms.AddAsync(rooms);

        await _context.SaveChangesAsync(cancellationToken);


        return ServiceResult.Success(rooms);
    } 
}
