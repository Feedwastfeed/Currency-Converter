using DemoinLayer.Domin;
using DemoinLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer
{
    public class Repository<T> : IRepository<T> where T : BaseCurrency
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> entities;


        public Repository(ApplicationDbContext context)
        {
            this._context = context;
            entities = _context.Set<T>();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
        }

        public T Get(int id)
        {
            return entities.SingleOrDefault(e => e.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }

        public void Insert(T entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
        }

        public void SaveChange()
        {
            _context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
        }
    }
}
