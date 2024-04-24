
using Application.Common.Interfaces;
using ChatSrever.Domain.Entities;

namespace Chatserver.Application.Queries;

/*public class GetAllMessagesQuery : IRequest<IEnumerable<User>>
{

    public class GetAllMessagesQueryHandler : IRequestHandler<GetAllMessagesQuery, IEnumerable<User>>
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
*/