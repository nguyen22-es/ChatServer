

using Application.Common.Interfaces;
using AutoMapper;
using Chatserver.Application.Common.Models;
using ChatServer.Domain.Events;
using ChatSrever.Domain.Entities;
using Domain.Events;
using Domain.Events.RoomEvent;
using System.Data.SqlTypes;

namespace Application.Authentication.Commands;

public record AddUserRoomRoomUserCommand : IRequest
{
    public int  RoomId{ get; set; }

    public List<int> UsersId { get; set; }
}

public class AddUserRoomRoomUserCommandHandler : IRequestHandler<AddUserRoomRoomUserCommand>
{
    private readonly IApplicationDbContext _context;




    public AddUserRoomRoomUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(AddUserRoomRoomUserCommand request, CancellationToken cancellationToken)
    {
       
        foreach (var user in request.UsersId)
        {
            var roomUser = new RoomUser()
            {
                RoomId = request.RoomId,
                UserId = user,
            };

            await _context.RoomUsers.AddAsync(roomUser);

            await _context.SaveChangesAsync(cancellationToken);


        }



    }
}
