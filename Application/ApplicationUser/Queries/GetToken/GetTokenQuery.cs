using Application.Common.Interfaces;
using AutoMapper;
using Chatserver.Application.Common.Models;
using Chatserver.Application.Queries;
using ChatSrever.Domain.Entities;
using ChatServer.Application.ApplicationUser.Queries.GetToken;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Application.Dot;


namespace Chatserver.Application.ApplicationUser.Queries.GetToken
{
    public class GetTokenQuery :IRequestWrapper<LoginResponse>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }

    public class GetTokenQueryHandler : IRequestHandlerWrapper<GetTokenQuery, LoginResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public GetTokenQueryHandler(  ITokenService tokenService, IApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<ServiceResult<LoginResponse>> Handle(GetTokenQuery request, CancellationToken cancellationToken)
        {
            var isValid = await _context.Users.Where(a => a.account == request.UserName && a.Password == request.Password).FirstOrDefaultAsync();

            if (isValid == null)
                return ServiceResult.Failed<LoginResponse>(ServiceError.UserNotFound);


            return ServiceResult.Success(new LoginResponse
            {
                User = _mapper.Map<UserDot>(isValid),
                Token = _tokenService.CreateJwtSecurityToken(isValid.Id)
            });
        }

    }
}
