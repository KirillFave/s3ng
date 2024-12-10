using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.EFCore;

public class UserServiceContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public UserServiceContext(DbContextOptions<UserServiceContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}