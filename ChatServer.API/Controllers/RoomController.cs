using Application.Authentication.Commands;
using Application.Common.Interfaces;
using Chatserver.Application.ApplicationUser.Queries.GetToken;
using Chatserver.Application.Common.Models;
using Chatserver.Application.Queries;
using Chatserver.Application.UserC.Queries.GetTodoItemsWithPagination;
using ChatServer.Api.Controllers;
using ChatSrever.Domain.Entities;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using ServerSingalr.ChatHub;

namespace ChatServer.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RoomController : BaseApiController
    {
     
        private readonly HubService _hubConnection;
    
 

        public RoomController(HubService hubConnection) 
        {
            _hubConnection = hubConnection;
         
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetRoom(int request) // lấy ra những phòng của user tham gia
        {
          
                
            var authResult = await Mediator.Send(new GetRoomsByIdQuery(request));



            return Ok(authResult);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateRoomTrue(CreateRoomTrueCommand request) // tạo room chat với bạn bè
        {


            var authResult = await Mediator.Send(request);

            var connect = _hubConnection._HubConnection;
            foreach (var item in request.ListId)
            {


             await  connect.InvokeAsync("Join", authResult.Data, item);
                Console.WriteLine(item + "đã tham gia vào phòng" + request);
            }

            return Ok(authResult);
        }

        [HttpPost("Check")]
        public async Task<ActionResult> checkRoomFrend(GetMessageCheckRoomFriend request) // check xem mình với bạn bè đã chat với nhau hay chưa rôi thì get tin nhăn chưa thì tạo room chat voi bạn bè
        {



            var authResult = await Mediator.Send(request);
            var connect = _hubConnection._HubConnection;
            await connect.InvokeAsync("Join", authResult.Data, request.FriendId);
            await connect.InvokeAsync("Join", authResult.Data, request.UserId);

            return Ok(authResult);
        }


        [HttpPost("AddUserRoom")]
        public async Task<IActionResult> AddUserRoom(AddUserRoomUserCommand request) // thêm user vào phòng chat
        {
           
          var authResult =  await Mediator.Send(request);
            var connect = _hubConnection._HubConnection;
            foreach (var item in request.UsersId)
            {
                await connect.InvokeAsync("Join", authResult, item);
                Console.WriteLine( item + "đã tham gia vào phòng"+ request.RoomId);
            }
          
            return Ok();
        }

    }
}
