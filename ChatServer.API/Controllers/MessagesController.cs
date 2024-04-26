using Application.Authentication.Commands;
using Chatserver.Application.ApplicationUser.Queries.GetToken;
using Chatserver.Application.Queries;
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


        [HttpPost("Get")]
        public async Task<IActionResult> GetAllMessages(GetAllMessagesQuery request)
        {

            var authResult = await Mediator.Send(request);



            return Ok(authResult);
        }


        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteMessages(DeleteMessagesCommand request)
        {

           var result =  await Mediator.Send(request);



            return Ok(result);
        }


        [HttpPost("Delete")]
        public async Task<IActionResult> PostMessages(UpdateMessagesCommand request)
        {

            var result = await Mediator.Send(request);



            return Ok(result);
        }
    }
}
