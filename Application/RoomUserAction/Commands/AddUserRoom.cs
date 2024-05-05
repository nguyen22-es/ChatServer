

using Application.Common.Interfaces;
using AutoMapper;
using Chatserver.Application.Common.Models;
using ChatServer.Domain.Events;
using ChatSrever.Domain.Entities;
using Domain.Events;
using Domain.Events.RoomEvent;
using MediatR;
using System.Data.SqlTypes;

namespace Application.Authentication.Commands;

public record AddUserRoomUserCommand : IRequest
{
    public int  RoomId{ get; set; }

    public List<int> UsersId { get; set; }
}

public class AddUserRoomRoomUserCommandHandler : IRequestHandler<AddUserRoomUserCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;



    public AddUserRoomRoomUserCommandHandler(IApplicationDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task Handle(AddUserRoomUserCommand request, CancellationToken cancellationToken)
    {
       
        foreach (var user in request.UsersId)
        {
            var roomUser = new RoomUser()
            {
                RoomId = request.RoomId,
                UserId = user,
            };

            var name =  _context.Users.AsNoTracking().FirstOrDefault(u => u.Id == user)?.Name;

            await _context.RoomUsers.AddAsync(roomUser);

            await _context.SaveChangesAsync(cancellationToken);

            await  _mediator.Publish(new RoomCompletedEvent(new Rooms {Title = name + " đã được thêm vào phòng chat",Id= request.RoomId }));;


        }



    }
}
