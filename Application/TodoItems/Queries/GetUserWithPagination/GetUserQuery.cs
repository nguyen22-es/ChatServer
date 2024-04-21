
using ChatServer.Application.Common.Interfaces;
using ChatSrever.Domain.Entities;

namespace Chatserver.Application.TodoItems.Queries.GetTodoItemsWithPagination;

public class GetAllUsersQuery : IRequest<IEnumerable<User>>
{

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
    {
        private readonly IApplicationDbContext _context;
        public GetAllUsersQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            var UserList = await _context.Users.ToListAsync();
            if (UserList == null)
            {
                return null;
            }
            return UserList.AsReadOnly();
        }
    }
}
