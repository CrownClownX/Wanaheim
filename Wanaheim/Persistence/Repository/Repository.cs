using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Wanaheim.Core.Repository;

namespace Wanaheim.Persistence.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext Context;

        public Repository(DbContext context)
        {
            Context = context;
        }

        public virtual async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return await Context.Set<TEntity>()
                .Where(predicate)
                .ToListAsync();
        }

        public virtual async Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate)  
        {
            return await Context.Set<TEntity>()
                .SingleOrDefaultAsync(predicate);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll()
        {
            return await Context.Set<TEntity>()
                .ToListAsync();
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }
    }
}
