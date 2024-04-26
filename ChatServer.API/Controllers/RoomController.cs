using Application.Authentication.Commands;
using Chatserver.Application.ApplicationUser.Queries.GetToken;
using Chatserver.Application.Common.Models;
using Chatserver.Application.UserC.Queries.GetTodoItemsWithPagination;
using ChatServer.Api.Controllers;
using ChatSrever.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChatServer.API.Controllers
{
    public class RoomController : BaseApiController
    {
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetRoom(int request)
        {
            var GetRoomsByIdQuery = new GetRoomsByIdQuery();

            GetRoomsByIdQuery.UserId = request; 

            var authResult = await Mediator.Send(GetRoomsByIdQuery);



            return Ok(authResult);
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CreateRoomTrue(CreateRoomTrueCommand request) // tạo room chat với bạn bè
        {
          


            var authResult = await Mediator.Send(request);



            return Ok(authResult);
        }

    }
}
