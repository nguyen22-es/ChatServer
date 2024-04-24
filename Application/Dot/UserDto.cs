using AutoMapper;
using ChatSrever.Domain.Entities;


namespace Application.Dot;

public class UserDot
{


    public string account { get; set; }

    public string Password { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, UserDot>();
        }
    }
}
