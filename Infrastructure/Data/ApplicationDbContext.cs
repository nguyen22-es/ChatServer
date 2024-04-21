using ChatServer.Application.Common.Interfaces;
using ChatSrever.Domain.Common;
using ChatSrever.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;


namespace ChatServer.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{

    private readonly IDateTime _dateTime;
    private readonly ICurrentUserService _currentUserService;


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, 
        ICurrentUserService currentUserService,
            IDateTime dateTime) : base(options) 
    {
        _dateTime = dateTime;
        _currentUserService = currentUserService;

    }

    public DbSet<Rooms> Rooms { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RoomMessages> RoomMessages { get; set; }
    public DbSet<Messages> Messages { get; set; }
    public DbSet<RoomUser> RoomUsers { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseAuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                   // entry.Entity.Created = _currentUserService.UserId;
                    entry.Entity.Created = _dateTime.Now;
                    break;
                case EntityState.Modified:
                 //   entry.Entity.LastModifiedBy = _currentUserService.UserId;
                    entry.Entity.LastModified = _dateTime.Now;
                    break;
            }
        }

        var result = await base.SaveChangesAsync(cancellationToken);
      


        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
  
}
