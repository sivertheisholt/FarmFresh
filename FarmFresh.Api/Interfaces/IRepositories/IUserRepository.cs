using FarmFresh.Api.Entities;

namespace FarmFresh.Api.Interfaces.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<List<User>> GetAll();
        Task<User?> GetUserById(int id);
        Task<User?> GetUserByUid(string uid);
    }
}