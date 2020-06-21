using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using restWithAspNetCore.Model;
using restWithAspNetCore.Model.Context;
using restWithAspNetCore.Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace restWithAspNetCore.Businnes.Implementations
{

    public class PersonRepository : IPersonRepository
    {
        private MySQLContext _context;

        public PersonRepository(MySQLContext context)
        {
            _context = context;
        }

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return person;
        }

        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
            try
            {
                if (result != null)
                {
                    _context.Persons.Remove(result);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }
        public Person FindById(long id)
        {
            return _context.Persons.SingleOrDefault(person => person.Id.Equals(id));
        }

        public Person Update(Person person)
        {
            if (!Exist(person.Id)) return null;

            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));
            try
            {
                _context.Entry(result).CurrentValues.SetValues(person);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return person;
        }

        public bool Exist(long? id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }


    }
}
