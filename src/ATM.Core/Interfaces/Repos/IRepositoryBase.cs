using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ATM.Core.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        T GetById(int id);

        T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);

        IEnumerable<T> GetMultiple(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties);

        IEnumerable<T> GetAll();

        T Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}