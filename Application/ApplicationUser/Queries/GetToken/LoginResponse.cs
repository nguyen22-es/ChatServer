using Chatserver.Application.UserC.Queries;

namespace ChatServer.Application.ApplicationUser.Queries.GetToken
{
    public class LoginResponse
    {
        public UserDto User { get; set; }

        public string Token { get; set; }
    }
}
