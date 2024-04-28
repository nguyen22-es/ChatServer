using Application.Authentication.Commands;
using Application.Common.Interfaces;
using Chatserver.Application.ApplicationUser.Queries.GetToken;
using Chatserver.Application.Common.Models;
using Chatserver.Application.UserC.Queries.GetTodoItemsWithPagination;
using ChatServer.Api.Controllers;
using ChatSrever.Domain.Entities;
using Infrastructure.ChatHub;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatServer.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RoomController : BaseApiController
    {
        private readonly ICurrentUserService _user;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IMediator mediator;

        public RoomController(ICurrentUserService currentUserService, IHubContext<ChatHub> hubContext) 
        {
            _user = currentUserService;
            _hubContext = hubContext;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetRoom(int request)
        {
          
                
            var authResult = await Mediator.Send(new GetRoomsByIdQuery(request));



            return Ok(authResult);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateRoomTrue(CreateRoomTrueCommand request) 
        {
          


            var authResult = await Mediator.Send(request);



            return Ok(authResult);
        }

        [HttpPost("AddUserRoom")]
        public async Task<IActionResult> AddUserRoom(AddUserRoomRoomUserCommand request) // tạo room chat với bạn bè
        {
            var TaskHub = new ChatHub(mediator);
            await Mediator.Send(request);

             foreach(var item in request.UsersId)
            {
               await TaskHub.Join(request.RoomId.ToString(),item);
                Console.WriteLine( item + "đã tham gia vào phòng"+ request.RoomId);
            }
          
            return Ok();
        }

    }
}
