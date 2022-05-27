using DemoinLayer.Domin;
using DemoinLayer.Repository;
using DemoinLayer.Service;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLayer
{
    public class BaseService<T> : IBaseService<T> where T : BaseCurrency
    {
        private readonly IRepository<T> _repository;

        public BaseService(IRepository<T> repository)
        {
            this._repository = repository;
        }
        public void Delete(T entity)
        {
            _repository.Delete(entity);
            _repository.SaveChange();
        }

        public T Get(int id)
        {
            return _repository.Get(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll();
        }

        public void Insert(T entity)
        {
            _repository.Insert(entity);
            _repository.SaveChange();
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
            _repository.SaveChange();
        }
    }
}
