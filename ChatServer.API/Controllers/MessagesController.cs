using Application.Authentication.Commands;
using Chatserver.Application.ApplicationUser.Queries.GetToken;
using ChatServer.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ChatServer.API.Controllers
{
    public class MessagesController : BaseApiController
    {
        [HttpPost("Create")]
        public async Task<IActionResult> CreateMessages(CreateMessagesCommand request)
        {

            var authResult = await Mediator.Send(request);



            return Ok(authResult);
        }
    }
}
