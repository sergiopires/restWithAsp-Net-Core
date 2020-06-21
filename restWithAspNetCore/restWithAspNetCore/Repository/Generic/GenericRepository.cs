using Microsoft.EntityFrameworkCore;
using restWithAspNetCore.Model.Base;
using restWithAspNetCore.Model.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace restWithAspNetCore.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private MySQLContext _context;
        private DbSet<T> dataset;


        public GenericRepository(MySQLContext context)
        {
            _context = context;
            dataset = context.Set<T>();
        }

        public T Create(T item)
        {
            try
            {
                dataset.Add(item);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return item;
        }

        public void Delete(long id)
        {
            var result = dataset.SingleOrDefault(p => p.Id.Equals(id));
            try
            {
                if (result != null)
                {
                    dataset.Remove(result);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Exist(long? id)
        {
            return dataset.Any(p => p.Id.Equals(id));
        }

        public List<T> FindAll()
        {
            return dataset.ToList();
        }

        public T FindById(long id)
        {
            return dataset.SingleOrDefault(p => p.Id.Equals(id));
        }

        public T Update(T item)
        {
            if (!Exist(item.Id)) return null;

            var result = dataset.SingleOrDefault(p => p.Id.Equals(item.Id));
            try
            {
                _context.Entry(result).CurrentValues.SetValues(result);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
