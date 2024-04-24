using System.Collections.Generic;
using System.Threading;
using Application.Dot;
using Chatserver.Application.Queries;
using Microsoft.AspNetCore.Mvc;


namespace ChatServer.Api.Controllers
{


    public class UserController : BaseApiController
    {
   
        [HttpGet]
        public async Task<ActionResult<List<UserDot>>> GetAllUser(CancellationToken cancellationToken)
        {
            //Cancellation token example.
            return Ok(await Mediator.Send(new GetAllUsersQuery(), cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDot>> GetCityById(GetUserByIdQuery id)
        {
            return Ok(await Mediator.Send(id));
        }

 
    }
}