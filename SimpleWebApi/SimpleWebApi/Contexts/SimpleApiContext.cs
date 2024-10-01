using Microsoft.EntityFrameworkCore;
using SimpleWebApi.Entities;

namespace SimpleWebApi.Contexts
{
    public class SimpleApiContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public SimpleApiContext(DbContextOptions<SimpleApiContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();

            builder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();
        }
    }
}
