using System.Linq.Expressions;

namespace TrilhaApiDesafio.Common.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T GetById(object id);
        T GetById(Expression<Func<T, bool>> keySelector, params Expression<Func<T, object>>[] includes);
        T GetByIdAsNoTracking(object id);

        IEnumerable<T> GetAll();
        IEnumerable<T> Get(
           Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           params Expression<Func<T, object>>[] includeProperties);
        Task<IEnumerable<T>> GetAsync(
           Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           params Expression<Func<T, object>>[] includeProperties);
        bool Any(Expression<Func<T, bool>> filter = null);
        IEnumerable<T> GetWithRawSql(string query, params object[] parameters);

        T Create(T entidade);
        void Update(T entidade);
        void Delete(object id);
        void Delete(T entidade);

        void Save();
        void Dispose();
    }
}
