using AutoMapper;
using ChatSrever.Domain.Entities;


namespace Chatserver.Application.TodoItems.Queries.GetTodoItemsWithPagination;

public class UserDto
{
    public int Id { get; init; }

    public string Name { get; set; }

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
