using AutoMapper;
using ChatSrever.Domain.Entities;


namespace Application.Dot;

public class UserDot
{

    public int Id { get; set; }
    public string account { get; set; }

    public string Name { get; set; }

    public string AvatarImageUrl { get; set; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, UserDot>();
        }
    }
}
