using AutoMapper;
using ChatSrever.Domain.Entities;


namespace Chatserver.Application.UserC.Queries;

public class UserDto
{
  

    public string account { get; set; }

    public string Password { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, UserDto>();
        }
    }
}
