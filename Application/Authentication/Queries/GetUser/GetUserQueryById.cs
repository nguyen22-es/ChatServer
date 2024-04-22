using Application.Common.Interfaces;
using AutoMapper;
using Chatserver.Application.Common.Models;


namespace Chatserver.Application.UserC.Queries.GetTodoItemsWithPagination;

public class GetUserByIdQuery : IRequestWrapper<UserDto>
{
    public int Id { get; set; }
}

    public class GetUserByIdQueryHandler : IRequestHandlerWrapper<GetUserByIdQuery, UserDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;



        public GetUserByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }


        public async Task<ServiceResult<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
        var user = await _context.Users.Where(a => a.Id == request.Id).FirstOrDefaultAsync();
        if (user == null)
            return ServiceResult.Failed<UserDto>(ServiceError.ForbiddenError);


        return ServiceResult.Success(
        
             _mapper.Map<UserDto>(user)
          );

   
    }


    /*   public GetUserByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
       {
           _mapper = mapper;
           _context = context;
       }
       public async Task<UserDto> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
       {
           var user = await _context.Users.Where(a => a.Id == query.Id).FirstOrDefaultAsync();
           if (user == null) return null;

           var userDOT = _mapper.Map<UserDto>(user);
           return userDOT;
       }*/



}


