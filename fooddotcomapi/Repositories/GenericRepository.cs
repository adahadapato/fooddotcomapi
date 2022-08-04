using fooddotcomapi.Data;
using fooddotcomapi.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace fooddotcomapi.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context = null;
        private readonly DbSet<T> table = null;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }
        public async Task<bool> AddAsync(T obj)
        {
            table.Add(obj);
            var result = await SaveAsync();
            return Convert.ToBoolean(result);
        }

        public async Task<bool> AddAsync(IEnumerable<T> obj)
        {
            table.AddRange(obj);
            var result = await SaveAsync();
            return Convert.ToBoolean(result);
        }

        public async Task<int> CountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }

        public async Task<bool> DeleteAsnc(T obj)
        {
            table.Remove(obj);
            var result = await SaveAsync();
            return Convert.ToBoolean(result);
        }

        public async Task<bool> DeleteAsync(List<T> obj)
        {
            table.RemoveRange(obj);
            var result = await SaveAsync();
            return Convert.ToBoolean(result);
        }
        public async Task<bool> DeleteAsync(object id)
        {
            T existing = table.Find(id);
            table.Remove(existing);
            var result = await SaveAsync();
            return Convert.ToBoolean(result);
        }

        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(object Id)
        {
            var result = await table.FindAsync(Id);
            return result;
        }

        public async Task<IList<T>> GetAsync(Expression<Func<T, bool>> predicate, params string[] navigationProperties)
        {
            List<T> list;
            var query = _context.Set<T>().AsQueryable();

            foreach (string navigationProperty in navigationProperties)
                query = query.Include(navigationProperty);//got to reaffect it.

            list = await query.Where(predicate).ToListAsync<T>();
            return list;
        }

        public async Task<IList<T>> GetDistinct(Expression<Func<T, bool>> predicate, params string[] navigationProperties)
        {
            List<T> list;
            var query = _context.Set<T>().AsQueryable();

            foreach (string navigationProperty in navigationProperties)
                query = query.Include(navigationProperty);//got to reaffect it.

            list = await query.Where(predicate).Distinct().ToListAsync<T>();
            return list;
        }

        public async Task<IEnumerable<T>> GetAsync()
        {
            return await table.ToListAsync();
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public IQueryable<T> GetAllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = GetAll();
            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {

                queryable = queryable.Include<T, object>(includeProperty);
            }
            return queryable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> FindAsync(object Id)
        {
            var result = await table.FindAsync(Id);
            return result != null;
        }

        public async Task<ICollection<T>> FindAllAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().Where(match).ToListAsync();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = _context.Set<T>().Where(predicate);
            return query;
        }

        public virtual async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(match);
        }


        private async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public Task<bool> Find(object Id)
        {
            throw new NotImplementedException();
        }

        public Task<T> Get(object Id)
        {
            throw new NotImplementedException();
        }

        public Task<IList<T>> GetSP(string query)
        {
            throw new NotImplementedException();
        }

        public Task<T> Get(int Name, Expression<Func<T, bool>> predicate, params string[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public Task<IList<T>> Get(Expression<Func<T, bool>> predicate, params string[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Add(T obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Add(IEnumerable<T> obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(List<T> obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(T obj)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(T obj)
        {
            throw new NotImplementedException();
        }
    }
}

