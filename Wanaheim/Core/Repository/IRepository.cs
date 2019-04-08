using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Wanaheim.Core.Repository
{
    public interface IRepository<TEntity> where TEntity : class 
    {
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAll();

        void Add(TEntity entity);
        void Remove(TEntity entity);
    }
}
