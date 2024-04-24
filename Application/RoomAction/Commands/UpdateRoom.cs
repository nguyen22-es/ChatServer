

using Application.Common.Interfaces;
using AutoMapper;
using Chatserver.Application.Common.Models;
using ChatServer.Domain.Events;
using ChatSrever.Domain.Entities;

namespace Application.Authentication.Commands;

public record UpdateRoomCommand : IRequestWrapper<Rooms>
{
    public int Id { get; set; }

    public string Title { get; set; }

    public int QuantityUser { get; set; }


}

public class UpdateRoomCommandHandler : IRequestHandlerWrapper<UpdateRoomCommand, Rooms>
{
    private readonly IApplicationDbContext _context;



    public UpdateRoomCommandHandler(IApplicationDbContext context)
    {
        _context = context;

    }

    public async Task<ServiceResult<Rooms>> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Rooms.FindAsync(request);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Rooms), request.Id.ToString());
        }
        if (!string.IsNullOrEmpty(request.Id.ToString()))
        entity.Title = request.Title;
        entity.QuantityUser = request.QuantityUser;

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(entity);
    }
}
