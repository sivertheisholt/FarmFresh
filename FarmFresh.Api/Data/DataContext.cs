using FarmFresh.Api.Entities;
using FarmFresh.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FarmFresh.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> User { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            /*********** User **************/
            builder.Entity<User>()
                .HasKey(User => User.UserId);
        }
    }
}