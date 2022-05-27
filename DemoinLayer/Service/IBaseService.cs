using DemoinLayer.Domin;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoinLayer.Service
{
    public interface IBaseService<T> where T : BaseCurrency
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);

    }
}
