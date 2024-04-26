using Application.Common.Interfaces;
using ChatSrever.Domain.Common;
using ChatSrever.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;


namespace ChatServer.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
 


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options 
    ) : base(options) 
    {
      

    }

    public DbSet<Rooms> Rooms { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RoomMessages> RoomMessages { get; set; }
    public DbSet<Messages> Messages { get; set; }
    public DbSet<RoomUser> RoomUsers { get; set; }

   

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }



}
