
using Application.Common.Interfaces;
using Application.Dot;
using AutoMapper;
using ChatSrever.Domain.Entities;

namespace Chatserver.Application.Queries;

public class GetAllUsersQuery : IRequest<IEnumerable<UserDot>>
{

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDot>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllUsersQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserDot>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
        {
            var UserList = await _context.Users.ToListAsync();
            if (UserList == null)
            {
                return null;
            }
          var  User = _mapper.Map <List<UserDot>>(UserList);


            return User.AsReadOnly();
        }
    }
}
