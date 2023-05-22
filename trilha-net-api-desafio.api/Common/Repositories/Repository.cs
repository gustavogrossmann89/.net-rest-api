using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Common.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TrilhaApiDesafio.Common.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal OrganizadorContext context;
        internal DbSet<T> dbSet;

        public Repository(OrganizadorContext context)
        {
            this.context = context;
            this.context.ChangeTracker.LazyLoadingEnabled = false;
            this.dbSet = context.Set<T>();
        }

        public virtual T GetById(object id)
        {
            return dbSet.Find(id);
        }

        public virtual T GetById(Expression<Func<T, bool>> keySelector, params Expression<Func<T, object>>[] includes)
        {
            List<string> includelist = new List<string>();

            foreach (var item in includes)
            {
                if (!(item.Body is MemberExpression body))
                    throw new ArgumentException("The body must be a member expression");

                includelist.Add(body.Member.Name);
            }

            includelist.ForEach(x => dbSet.Include(x));

            return dbSet.First(keySelector);
        }

        public T GetByIdAsNoTracking(object id)
        {
            var entity = this.GetById(id);
            context.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual IEnumerable<T> GetWithRawSql(string query, params object[] parameters)
        {
            return dbSet.FromSqlRaw(query, parameters).ToList();
        }

        public virtual IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public async virtual Task<IEnumerable<T>> GetAsync(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public virtual bool Any(Expression<Func<T, bool>> filter = null)
        {
            if (filter != null)
                return dbSet.Any(filter);
            else
                return dbSet.Any();
        }

        public virtual T Create(T entidade)
        {
            context.Entry(entidade).State = EntityState.Detached;
            return dbSet.Add(entidade).Entity;
        }

        public virtual void Update(T entidade)
        {
            context.Entry(entidade).State = EntityState.Detached;
            context.Entry(entidade).State = EntityState.Modified;
        }

        public virtual void Delete(object id)
        {
            T entidade = dbSet.Find(id);
            Delete(entidade);
        }

        public virtual void Delete(T entidade)
        {
            context.Entry(entidade).State = EntityState.Detached;
            dbSet.Remove(entidade);
        }

        public virtual void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}