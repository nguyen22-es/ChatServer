

using Application.Common.Interfaces;
using AutoMapper;
using Chatserver.Application.Common.Models;
using ChatServer.Application.ApplicationUser.Queries.GetToken;
using ChatServer.Domain.Events;
using ChatSrever.Domain.Entities;

namespace Application.Authentication.Commands;

public record UpdateMessagesCommand : IRequestWrapper<Messages>
{
    public int MessageId { get; set; }

    public string content { get; set; }


}

public class UpdateMessagesCommandHandler : IRequestHandlerWrapper<UpdateMessagesCommand, Messages>
{
    private readonly IApplicationDbContext _context;




    public UpdateMessagesCommandHandler(IApplicationDbContext context)
    {
        _context = context;
   
    }

    public async Task<ServiceResult<Messages>> Handle(UpdateMessagesCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Messages.FindAsync(request);

        if (entity == null)
        {
            return ServiceResult.Failed<Messages>(ServiceError.NotFound);
        }
        if (!string.IsNullOrEmpty(request.MessageId.ToString()))
        entity.Content = request.content;

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(entity);
    }
}
