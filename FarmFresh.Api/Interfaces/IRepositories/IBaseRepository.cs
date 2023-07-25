namespace FarmFresh.Api.Interfaces.IRepositories
{
    public interface IBaseRepository<in T> where T : class
    {
        void Update(T entity);
        void Delete(T entity);
        Task ResetId(string tableName);
        void Add(T entity);
    }
}