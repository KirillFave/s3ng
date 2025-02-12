using IAM.Entities;
using Microsoft.EntityFrameworkCore;

namespace IAM.DAL
{
    internal class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        internal DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().Property(x => x.Email).IsRequired().HasMaxLength(128);
            modelBuilder.Entity<User>().Property(x => x.PasswordHash).IsRequired().HasMaxLength(128);
            modelBuilder.Entity<User>().Property(x => x.Role).IsRequired();

            modelBuilder.Entity<User>().HasIndex(x => x.Email).IsUnique();
            modelBuilder.Entity<User>().HasKey(x => x.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }
    }
}
