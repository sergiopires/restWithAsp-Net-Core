using restWithAspNetCore.Model;
using System.Collections.Generic;

namespace restWithAspNetCore.Repository.Implementations
{
    public interface IBookRepository
    {
        Book Create(Book book);
        Book FindById(long id);
        List<Book> FindAll();
        Book Update(Book book);
        void Delete(long id);

        bool Exist(long? id);
    }
}
