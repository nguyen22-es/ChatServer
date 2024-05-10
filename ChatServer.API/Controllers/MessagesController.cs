using Application.Authentication.Commands;
using Chatserver.Application.ApplicationUser.Queries.GetToken;
using Chatserver.Application.Queries;
using ChatServer.Api.Controllers;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using ServerSingalr.ChatHub;
using System;

namespace ChatServer.API.Controllers
{

    public class MessagesController : BaseApiController
    {
        private readonly HubService _hubConnection;

        public MessagesController(HubService hubConnection)
        {
            _hubConnection = hubConnection;   
        }
        [HttpPost("Create")]
        public async Task<IActionResult> CreateMessages(CreateMessagesCommand request)
        {

            var authResult = await Mediator.Send(request);
             if (authResult.Succeeded) 
            {
                 await _hubConnection._HubConnection.InvokeAsync("creatMessages", authResult.Data, request.RoomId);
  
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


        [HttpPost("Update")]
        public async Task<IActionResult> PostMessages(UpdateMessagesCommand request)
        {

            var result = await Mediator.Send(request);



            return Ok(result);
        }
    }
}
