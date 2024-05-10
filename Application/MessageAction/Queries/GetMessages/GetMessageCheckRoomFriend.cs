
using Application.Common.Interfaces;
using Application.Dot;
using ChatServer.Application.ApplicationUser.Queries.GetToken;
using Chatserver.Application.Common.Models;
using ChatSrever.Domain.Entities;
using FluentValidation.Validators;
using Application.Authentication.Commands;
using AutoMapper;

namespace Chatserver.Application.Queries;

public record GetMessageCheckRoomFriend(int UserId,int FriendId) : IRequestWrapper<RoomCheckDot>;


public class GetMessageRoomFriendHandler : IRequestHandlerWrapper<GetMessageCheckRoomFriend, RoomCheckDot>
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator mediator;
    private readonly IMapper _mapper;


    public GetMessageRoomFriendHandler(IApplicationDbContext context, IMediator mediator,IMapper mapper)
    {
        _mapper = mapper;
        _context = context;
        this.mediator = mediator;   
    }

    public async Task<ServiceResult<RoomCheckDot>> Handle(GetMessageCheckRoomFriend request, CancellationToken cancellationToken)
    {
        var RoomUser = await _context.RoomUsers.AsNoTracking().Include(r => r.Rooms).Where(a => a.UserId == request.UserId && a.Rooms.IsGroup == false).ToListAsync();
        var RoomFriend = await _context.RoomUsers.AsNoTracking().Where(a => a.UserId == request.UserId).ToListAsync();

        var room = new RoomUser();
        var roomDot = new RoomCheckDot();
        var isExist = true;

        foreach (var item in RoomUser)
        {


            if (RoomFriend.Any(a => a.RoomId == item.RoomId))
            {
                room = item;
                isExist = false; // Đánh dấu khi tìm thấy phần tử trùng
                break; // Dừng lại ngay sau khi tìm thấy phần tử trùng
            }

        }

        IList<MessagesDot> messagesDot = new List<MessagesDot>();
        if (isExist)
        {
          var roomNew =  await mediator.Send(new CreateRoomTrueCommand { latestMessage = "", ListId = new List<int> { request.UserId, request.FriendId }, type = false, QuantityUser = 2 });
          
            roomDot.RoomDot = _mapper.Map<RoomDot>(roomNew.Data);
            roomDot.messagesDot = messagesDot;

            return ServiceResult.Success(roomDot);
        }




        var mesage = await mediator.Send(new GetAllMessagesQuery(room.RoomId));
        

        roomDot.RoomDot = _mapper.Map<RoomDot>(room.Rooms) ;
        roomDot.messagesDot = mesage.Data;




        return ServiceResult.Success(roomDot);

    }

}
