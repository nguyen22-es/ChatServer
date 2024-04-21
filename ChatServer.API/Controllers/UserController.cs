using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Chatserver.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using ChatServer.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Exchange.WebServices.Data;

namespace CleanArchitecture.Api.Controllers
{
    /// <summary>
    /// Cities
    /// </summary>
    [Authorize]
    public class UserController : BaseApiController
    {
        /// <summary>
        /// Get all cities
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<ServiceResult<List<UserDto>>>> GetAllUser(CancellationToken cancellationToken)
        {
            //Cancellation token example.
            return Ok(await Mediator.Send(new GetAllUsersQuery(), cancellationToken));
        }

        /// <summary>
        /// Get city by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResult<UserDto>>> GetCityById(int id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetUserByIdQuery { Id = id }, cancellationToken));
        }

 
    }
}