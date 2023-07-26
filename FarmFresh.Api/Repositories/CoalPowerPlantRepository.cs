using FarmFresh.Api.Data;
using FarmFresh.Api.Entities;
using FarmFresh.Api.Interfaces.IRepositories;

namespace FarmFresh.Api.Repositories
{
    public class CoalPowerPlantRepository : BaseRepository<CoalPowerPlant>, ICoalPowerPlantRepository
    {
        public CoalPowerPlantRepository(DataContext context) : base(context)
        {
        }
    }
}