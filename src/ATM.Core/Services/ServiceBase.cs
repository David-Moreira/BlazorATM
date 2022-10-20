using ATM.Core.Interfaces;
using ATM.Core.Interfaces.Services;
using System.Collections.Generic;

namespace ATM.Core.Services
{
    public class ServiceBase<T> : IServiceBase<T> where T : class
    {
        private readonly IRepositoryBase<T> _repo;

        public ServiceBase(IRepositoryBase<T> repo)
        {
            _repo = repo;
        }

        public virtual T Add(T entity)
        {
            return _repo.Add(entity);
        }

        public virtual void Delete(T entity)
        {
            _repo.Delete(entity);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _repo.GetAll();
        }

        public virtual T GetById(int id)
        {
            return _repo.GetById(id);
        }

        public virtual void Update(T entity)
        {
            _repo.Update(entity);
        }
    }
}