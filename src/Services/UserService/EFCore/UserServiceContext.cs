using Microsoft.EntityFrameworkCore;
using UserService.EFCore.Entities;

namespace UserService.EFCore;

public class UserServiceContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public UserServiceContext(DbContextOptions<UserServiceContext> options) : base(options)
    {
    }
}