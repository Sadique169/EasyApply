using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using EasyApply.Core.IRepositories;
using EasyApply.Core;

namespace PortfolioProject.Core.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _entities;

        public Repository(AppDbContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        public T Get(int id)
        {
            return _entities.Find(id);
        }

        public async Task<T> GetAsync(int id)
        {
            return await _entities.FindAsync(id);
        }

        public List<T> Get()
        {
            return _entities.ToList();
        }
        public async Task<List<T>> GetAsync()
        {
            return await _entities.ToListAsync();
        }

        public IQueryable<T> GetAll()
        {
            return _entities.AsQueryable();
        }

        public List<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _entities.Where(predicate).ToList();
        }

        public async Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entities.Where(predicate).ToListAsync();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _entities.FirstOrDefault(predicate);
        }

        public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entities.FirstOrDefaultAsync(predicate);
        }

        public T SingleOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _entities.SingleOrDefault(predicate);
        }

        public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _entities.SingleOrDefaultAsync(predicate);
        }

        public void Add(T entity)
        {
            _entities.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _entities.AddRange(entities);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            _entities.UpdateRange(entities);
        }

        public void Remove(T entity)
        {
            _entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _entities.RemoveRange(entities);
        }
    }


}
