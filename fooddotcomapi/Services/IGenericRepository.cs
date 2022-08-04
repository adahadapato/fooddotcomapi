using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace fooddotcomapi.Services
{
    public interface IGenericRepository<T> where T : class
    {
        Task<bool> Find(object Id);
        Task<T> Get(object Id);
        Task<IList<T>> GetSP(string query);
        Task<T> Get(int Name, Expression<Func<T, bool>> predicate, params string[] navigationProperties);
        Task<IList<T>> GetDistinct(Expression<Func<T, bool>> predicate, params string[] navigationProperties);
        Task<IList<T>> Get(Expression<Func<T, bool>> predicate, params string[] navigationProperties);
        Task<IEnumerable<T>> Get();
        Task<bool> Add(T obj);
        Task<bool> Add(IEnumerable<T> obj);
        //void Delete(T obj);
        Task<bool> Delete(List<T> obj);
        Task<bool> Delete(T obj);
        Task<bool> Delete(object id);
        Task<bool> Update(T obj);
        // void Save();
    }
}

