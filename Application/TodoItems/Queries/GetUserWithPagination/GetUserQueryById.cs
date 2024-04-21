using ChatServer.Application.Common.Interfaces;
using ChatSrever.Domain.Entities;

namespace Chatserver.Application.TodoItems.Queries.GetTodoItemsWithPagination;

public class GetUserByIdQuery : IRequest<User>
{
    public int Id { get; set; }
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IApplicationDbContext _context;
        public GetUserByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await _context.Users.Where(a => a.Id == query.Id).FirstOrDefaultAsync();
            if (product == null) return null;
            return product;
        }
    }
}
