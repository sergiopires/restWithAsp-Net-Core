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

    public class BookRepository : IBookRepository
    {
        private MySQLContext _context;

        public BookRepository(MySQLContext context)
        {
            _context = context;
        }

        public Book Create(Book book)
        {
            try
            {
                _context.Add(book);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return book;
        }

        public void Delete(long id)
        {


            var result = _context.Books.SingleOrDefault(p => p.Id.Equals(id));
            try
            {
                if (result != null)
                {
                    _context.Books.Remove(result);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Book> FindAll()
        {
            return _context.Books.ToList();
        }

        public Book FindById(long id)
        {
            return _context.Books.SingleOrDefault(book => book.Id.Equals(id));
        }

        public Book Update(Book book)
        {
            if (!Exist(book.Id)) return null;

            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(book.Id));
            try
            {
                _context.Entry(result).CurrentValues.SetValues(book);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
            return book;
        }

        public bool Exist(long? id)
        {
            return _context.Books.Any(p => p.Id.Equals(id));
        }


    }
}
