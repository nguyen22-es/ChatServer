using ChatSrever.Domain.Entities;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Rooms> Rooms { get; }

    DbSet<User> Users { get; }

    DbSet<RoomMessages> RoomMessages { get; }

    DbSet<Messages> Messages { get; }

    DbSet<RoomUser> RoomUsers { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
