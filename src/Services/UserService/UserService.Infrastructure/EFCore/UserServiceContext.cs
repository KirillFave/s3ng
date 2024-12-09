using Microsoft.EntityFrameworkCore;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.EFCore;

public class UserServiceContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public UserServiceContext(DbContextOptions<UserServiceContext> options) : base(options)
    {
    }
}