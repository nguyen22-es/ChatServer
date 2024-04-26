
using Application.Common.Interfaces;
using Application.Dot;
using ChatServer.Application.ApplicationUser.Queries.GetToken;
using Chatserver.Application.Common.Models;
using ChatSrever.Domain.Entities;
using FluentValidation.Validators;

namespace Chatserver.Application.Queries;

public class GetAllMessagesQuery : IRequestWrapper<IList<MessagesDot>>
{
    public int RoomId { get; set; }

    public string content { get; set; }
}

public class GetAllMessagesQueryHandler : IRequestHandlerWrapper<GetAllMessagesQuery, IList<MessagesDot>>
{
    private readonly IApplicationDbContext _context;
   

    public GetAllMessagesQueryHandler( IApplicationDbContext context)
    {
        _context = context;
       
    }

    public async Task<ServiceResult<IList<MessagesDot>>> Handle(GetAllMessagesQuery request, CancellationToken cancellationToken)
    {
        var isValid = await _context.RoomMessages.AsNoTracking().Where(a => a.RoomId == request.RoomId).Include(m => m.Messages ).ThenInclude(u => u.User).OrderByDescending(m => m.Messages.Created).ToListAsync();

        if (isValid == null)
            return ServiceResult.Failed<IList<MessagesDot>>(ServiceError.UserNotFound);


        IList<MessagesDot> messagesDot = new List<MessagesDot>();

        foreach (var item in isValid)
        {
            var messageDot = new MessagesDot
            {
                id = item.Messages.Id,
                content = item.Messages.Content,
                nameSend = item.Messages.User.Name,
                timeSend = item.Messages.Created

            };
            messagesDot.Add(messageDot);

        }



        return ServiceResult.Success(messagesDot);
        
    }

}
