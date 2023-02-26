using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null, string? includedProperties=null );
        void Add(T entity);
        T GetFirstOrDefault(Expression<Func<T, bool>> filter , string? includedProperties = null);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
}

