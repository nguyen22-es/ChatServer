using Application.Common.Interfaces;
using Chatserver.Application.Common.Models;


namespace Application.Authentication.Queries.CheckUser
{
    public class CheckCredentialsQuery : IRequest<ServiceResult<bool>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class CheckCredentialsQueryHandler : IRequestHandler<CheckCredentialsQuery, ServiceResult<bool>>
    {
        private readonly IApplicationDbContext _context;

        public CheckCredentialsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResult<bool>> Handle(CheckCredentialsQuery request, CancellationToken cancellationToken)
        {
            var isValid = await _context.Users.Where(a => a.account == request.Username && a.Password == request.Password).FirstOrDefaultAsync();

            if (isValid != null)
            {
                return ServiceResult<bool>.Success(true);
            }
            else
            {
                return ServiceResult<bool>.Success(false);
            }
        }
    }
}
