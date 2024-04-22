using System.Collections.Generic;
using System.Threading;
using Chatserver.Application.UserC.Queries;
using Chatserver.Application.UserC.Queries.GetTodoItemsWithPagination;
using ChatServer.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Exchange.WebServices.Data;

namespace ChatServer.Api.Controllers
{
 

    public class UserController : BaseApiController
    {
   
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetAllUser(CancellationToken cancellationToken)
        {
            //Cancellation token example.
            return Ok(await Mediator.Send(new GetAllUsersQuery(), cancellationToken));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetCityById(GetUserByIdQuery id)
        {
            return Ok(await Mediator.Send(id));
        }

 
    }
}