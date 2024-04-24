using System.Threading.Tasks;
using Application.Authentication.Commands;
using Chatserver.Application.ApplicationUser.Queries.GetToken;
using ChatServer.Api.Controllers;
using ChatServer.Application.ApplicationUser.Queries.GetToken;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ChatServer.Api.Controllers
{
    public class LoginController : BaseApiController
    {
    
         [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserCommand request)
        {
            return Ok(await Mediator.Send(request));
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(GetTokenQuery request)
        {
        
            var authResult = await Mediator.Send(request);

        

            return Ok(authResult);
        }



    }
}
