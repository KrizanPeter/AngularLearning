using System.Linq;
using System.Collections.Generic;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BoardGame.Domain.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll(
            Expression<Func<T,bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = null
        );

        Task<T> GetFirstOrDefault(
            Expression<Func<T,bool>> filter = null,
            string includeProperties = null
        );

        void Add(T entity);
        void Remove(int id);
        void Remove(T entity);
        void Save(); 
    }
}