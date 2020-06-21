using restWithAspNetCore.Model;
using restWithAspNetCore.Repository.Generic;
using restWithAspNetCore.Repository.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restWithAspNetCore.Businnes.Implementations
{
    public class BookBusinnesImpl : IBookBusinnes
    {
        // IBookRepository _repository;

        private IRepository<Book> _repository;
        public BookBusinnesImpl(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public Book Create(Book book)
        {
            return _repository.Create(book);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<Book> FindAll()
        {
            return _repository.FindAll();
        }

        public Book FindById(long id)
        {
            return _repository.FindById(id);
        }

        public Book Update(Book book)
        {
            return _repository.Update(book);
        }
    }
}
