using FarmFresh.Api.Data;
using FarmFresh.Api.Entities;
using FarmFresh.Api.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace FarmFresh.Api.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<User>> GetAll()
        {
            return await Context.User
                .Include(u => u.CoalPowerPlants)
                .Include(u => u.OrganicFertilizerFactory)
                .Include(u => u.OrganicSeedsFactory)
                .Include(u => u.PestAndDiseaseFactory)
                .Include(u => u.SoilAmendmentsFactory).ToListAsync();
        }
        public async Task<User?> GetUserById(int id)
        {
            return await Context.User
                .Include(u => u.CoalPowerPlants)
                .Include(u => u.OrganicFertilizerFactory)
                .Include(u => u.OrganicSeedsFactory)
                .Include(u => u.PestAndDiseaseFactory)
                .Include(u => u.SoilAmendmentsFactory)
                .SingleOrDefaultAsync(u => u.UserId == id);
        }
    }
}