using Application.Common.Interfaces;
using Chatserver.Application.Common.Models;
using ChatServer.Application.ApplicationUser.Queries.GetToken;
using ChatServer.Domain.Events;
using ChatSrever.Domain.Entities;

namespace Application.Authentication.Commands;

public record CreateUserCommand : IRequestWrapper<int>
{
    public string? Name { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? AvatarImageUrl { get; set; }
}

public class CreateUserCommandHandler : IRequestHandlerWrapper<CreateUserCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResult<int>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {

       

        var entity = new User
        {
            Name = request.Name,
            account = request.UserName,
            Password = request.Password,
            AvatarImageUrl = request.AvatarImageUrl
        };
        var user = await _context.Users.FirstOrDefaultAsync(u => u.account == entity.account);

        if (user != null)
            return ServiceResult.Failed<int>(ServiceError.ServiceProviderExist);


        entity.AddDomainEvent(new UserCreatedEvent(entity));

        _context.Users.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success( entity.Id);
    }
}
