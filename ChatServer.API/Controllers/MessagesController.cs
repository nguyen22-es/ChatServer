using Application.Authentication.Commands;
using Chatserver.Application.ApplicationUser.Queries.GetToken;
using Chatserver.Application.Queries;
using ChatServer.Api.Controllers;
using Infrastructure.ChatHub;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;

namespace ChatServer.API.Controllers
{

    public class MessagesController : BaseApiController
    {
        private readonly IHubContext<ChatHub> _hubContext;
        public MessagesController(IHubContext<ChatHub> hubContext) {
            _hubContext = hubContext;
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateMessages(CreateMessagesCommand request)
        {

            var authResult = await Mediator.Send(request);
             if (authResult.Succeeded) 
            {
                 await _hubContext.Clients.Group(request.RoomId.ToString()).SendAsync("newMessage", request);
               // await _hubContext.Clients.All.SendAsync("newMessage", request);
            } 
          

            return Ok(authResult);
        }


        [HttpGet("Get")]
        public async Task<IActionResult> GetAllMessages(int request)
        {
           
            var authResult = await Mediator.Send( new GetAllMessagesQuery(request));



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
