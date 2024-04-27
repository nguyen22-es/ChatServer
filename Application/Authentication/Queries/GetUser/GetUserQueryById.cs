using Application.Common.Interfaces;
using Application.Dot;
using AutoMapper;
using Chatserver.Application.Common.Models;


namespace Chatserver.Application.Queries;

public record GetUserByIdQuery(int Id) : IRequestWrapper<UserDot>;




    public class GetUserByIdQueryHandler : IRequestHandlerWrapper<GetUserByIdQuery, UserDot>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;



        public GetUserByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<ServiceResult<UserDot>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
        var user = await _context.Users.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
        if (user == null)
            return ServiceResult.Failed<UserDot>(ServiceError.ForbiddenError);


        return ServiceResult.Success(
        
             _mapper.Map<UserDot>(user)
          );

   
    }





}


