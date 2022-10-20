using ATM.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ATM.Infrastructure.Data
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly ATMDBContext _dbContext;

        public RepositoryBase()
        {
            _dbContext = new ATMDBContext();
        }

        public RepositoryBase(ATMDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T GetById(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public virtual T GetSingle(Func<T, bool> where,
         params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;

            IQueryable<T> dbQuery = _dbContext.Set<T>();

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            item = dbQuery.FirstOrDefault(where);

            return item;
        }

        public virtual IEnumerable<T> GetMultiple(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            IEnumerable<T> item = null;

            IQueryable<T> dbQuery = _dbContext.Set<T>();

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            item = dbQuery.Where(where);

            return item;
        }

        public IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>().ToList();
        }

        public T Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}