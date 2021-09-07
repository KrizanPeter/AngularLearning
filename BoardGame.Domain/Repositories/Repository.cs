using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using API.Entities.Context;
using BoardGame.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BoardGame.Domain.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DataContext _context;
        internal DbSet<T> dbSet;
        protected readonly ILogger<Repository<T>> _logger;

        public Repository(DataContext context)
        {
            _context = context;
            this.dbSet = context.Set<T>();
        }

        public async void Add(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task<T> Get(int id)
        {
            var result = await dbSet.FindAsync(id);
            return result;
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if(filter != null)
            {
                query = query.Where(filter);
            }
            //coma separated include properties
            if(includeProperties!= null)
            {
                foreach(var prop in includeProperties.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            if(orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetFirstOrDefault(Expression<Func<T, bool>> filter = null, string includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if(filter != null)
            {
                query = query.Where(filter);
            }
            //coma separated include properties
            if(includeProperties!= null)
            {
                foreach(var prop in includeProperties.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(prop);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async void Remove(int id)
        {
            T entityForRemove = await dbSet.FindAsync(id);
            Remove(entityForRemove);
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}