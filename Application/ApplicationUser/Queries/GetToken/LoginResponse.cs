using Application.Dot;
using Chatserver.Application.Queries;

namespace ChatServer.Application.ApplicationUser.Queries.GetToken
{
    public class LoginResponse
    {
        public UserDot User { get; set; }

        public string Token { get; set; }
    }
}
