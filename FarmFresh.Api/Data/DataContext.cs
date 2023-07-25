using Microsoft.EntityFrameworkCore;

namespace FarmFresh.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        //public DbSet<Visitor> Visitor { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {

        }
    }
}