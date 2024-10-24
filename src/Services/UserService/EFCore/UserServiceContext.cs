using Microsoft.EntityFrameworkCore;
using UserService.EFCore.Entities;

namespace UserService.EFCore;

public class UserServiceContext : DbContext
{
    public UserServiceContext(DbContextOptions<UserServiceContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }


}