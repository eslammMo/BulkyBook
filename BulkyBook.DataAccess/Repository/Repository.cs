using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBookWeb.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        readonly ApplicationDbContext _db;
        DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll(string? includedProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (includedProperties != null) {
                foreach (var includeProp in includedProperties.Split(new char[] {','},StringSplitOptions.RemoveEmptyEntries )) {
                    query = query.Include(includeProp); }

            } return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter , string? includedProperties = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            if (includedProperties != null)
            {
                foreach (var includeProp in includedProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }

            }

            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
