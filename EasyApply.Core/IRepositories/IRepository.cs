using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EasyApply.Core.IRepositories
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        Task<T> GetAsync(int id);

        List<T> Get();
        Task<List<T>> GetAsync();
        IQueryable<T> GetAll();

        List<T> Find(Expression<Func<T, bool>> predicate);

        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);

        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

        T SingleOrDefault(Expression<Func<T, bool>> predicate);
        Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate);

        void Add(T entity);
        void AddRange(IEnumerable<T> entities);

        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
