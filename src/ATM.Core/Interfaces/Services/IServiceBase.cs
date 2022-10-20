using System.Collections.Generic;

namespace ATM.Core.Interfaces.Services
{
    public interface IServiceBase<T> where T : class
    {
        T GetById(int id);

        IEnumerable<T> GetAll();

        T Add(T entity);

        void Update(T entity);

        void Delete(T entity);
    }
}