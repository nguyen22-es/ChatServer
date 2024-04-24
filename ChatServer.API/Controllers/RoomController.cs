using Application.Authentication.Commands;
using Chatserver.Application.ApplicationUser.Queries.GetToken;
using ChatServer.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ChatServer.API.Controllers
{
    public class RoomController : BaseApiController
    {
        [HttpPost("Create")]
        public async Task<IActionResult> CreateRoom(CreateRoomCommand request)
        {

            var authResult = await Mediator.Send(request);



            return Ok(authResult);
        }
    }
}
