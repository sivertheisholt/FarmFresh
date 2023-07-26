using FarmFresh.Api.Data;
using FarmFresh.Api.Entities;
using FarmFresh.Api.Interfaces.IRepositories;

namespace FarmFresh.Api.Repositories
{
    public class PestAndDiseaseFactoryRepository : BaseRepository<PestAndDiseaseFactory>, IPestAndDiseaseFactoryRepository
    {
        public PestAndDiseaseFactoryRepository(DataContext context) : base(context)
        {
        }
    }
}