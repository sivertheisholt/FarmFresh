using FarmFresh.Api.Entities;

namespace FarmFresh.Api.Interfaces.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<List<User>> GetAll();
    }
}