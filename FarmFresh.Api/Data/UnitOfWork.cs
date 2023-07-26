using FarmFresh.Api.Interfaces;
using FarmFresh.Api.Interfaces.IRepositories;
using FarmFresh.Api.Repositories;

namespace FarmFresh.Api.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository => new UserRepository(_context);
        public ICoalPowerPlantRepository CoalPowerPlantRepository => new CoalPowerPlantRepository(_context);
        public IOrganicFertilizerFactoryRepository OrganicFertilizerFactoryRepository => new OrganicFertilizerFactoryRepository(_context);
        public IOrganicSeedsFactoryRepository OrganicSeedsFactoryRepository => new OrganicSeedsFactoryRepository(_context);
        public IPestAndDiseaseFactoryRepository PestAndDiseaseFactoryRepository => new PestAndDiseaseFactoryRepository(_context);
        public ISoilAmendmentsFactoryRepository SoilAmendmentsFactoryRepository => new SoilAmendmentsFactoryRepository(_context);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanged()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}