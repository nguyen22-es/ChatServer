using Application.Authentication.Commands;
using Application.Common.Interfaces;
using Application.Dot;
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
  //  [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RoomController : BaseApiController
    {
     

        private readonly IHubContext<ChatHub> _hubClients;
 

        public RoomController(  IHubContext<ChatHub> hubClients) 
        {

            _hubClients = hubClients;
         
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


            await _hubClients.Clients.Group(authResult.Data.id.ToString()).SendAsync("AddUser", authResult.Data);

            return Ok(authResult);
        }

        [HttpPost("Check")]
        public async Task<ActionResult> checkRoomFrend(GetMessageCheckRoomFriend request) // check xem mình với bạn bè đã chat với nhau hay chưa rôi thì get tin nhăn chưa thì tạo room chat voi bạn bè
        {



            var authResult = await Mediator.Send(request);

            
            return Ok(authResult);
        }


        [HttpPost("AddUserRoom")]
        public async Task<IActionResult> AddUserRoom(AddUserRoomUserCommand request) // thêm user vào phòng chat
        {
           
          var authResult =  await Mediator.Send(request);

          await _hubClients.Clients.Group(authResult.id.ToString()).SendAsync("AddUser", authResult);

            return Ok();
        }

    }
}
