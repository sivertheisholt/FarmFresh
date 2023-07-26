using FarmFresh.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace FarmFresh.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> User { get; set; } = null!;
        public DbSet<CoalPowerPlant> CoalPowerPlant { get; set; } = null!;
        public DbSet<OrganicFertilizerFactory> OrganicFertilizerFactory { get; set; } = null!;
        public DbSet<OrganicSeedsFactory> OrganicSeedsFactory { get; set; } = null!;
        public DbSet<PestAndDiseaseFactory> PestAndDiseaseFactory { get; set; } = null!;
        public DbSet<SoilAmendmentsFactory> SoilAmendmentsFactory { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            /*********** User **************/
            builder.Entity<User>()
                .HasKey(user => user.UserId);

            /*********** CoalPowerPlant **************/
            builder.Entity<CoalPowerPlant>()
                .HasKey(coal => coal.PlantId);

            /*********** OrganicFertilizerFactory **************/
            builder.Entity<OrganicFertilizerFactory>()
                .HasKey(fact => fact.FactoryId);

            /*********** OrganicSeedsFactory **************/
            builder.Entity<OrganicSeedsFactory>()
                .HasKey(fact => fact.FactoryId);

            /*********** PestAndDiseaseFactory **************/
            builder.Entity<PestAndDiseaseFactory>()
                .HasKey(fact => fact.FactoryId);

            /*********** SoilAmendmentsFactory **************/
            builder.Entity<SoilAmendmentsFactory>()
                .HasKey(fact => fact.FactoryId);
        }
    }
}