using FarmFresh.Api.Interfaces.IRepositories;

namespace FarmFresh.Api.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        Task<bool> Complete();
        bool HasChanged();
    }
}