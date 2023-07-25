using FarmFresh.Api.Data;
using FarmFresh.Api.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace FarmFresh.Api.Repositories
{
    abstract public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly DataContext _context;
        protected BaseRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public async Task ResetId(string tableName)
        {
            await _context.Database.ExecuteSqlRawAsync($"DBCC CHECKIDENT ('{tableName}', RESEED, 0)");
        }

        protected DataContext Context { get { return _context; } }
    }
}