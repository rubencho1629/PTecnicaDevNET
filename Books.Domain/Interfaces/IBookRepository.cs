using System.Collections.Generic;
using Books.Domain.Entities;

namespace Books.Domain.Interfaces
{
    public interface IBookRepository
    {
        int Count();

        Book GetById(int id);
        IEnumerable<Book> GetAll();

        void Add(Book book);
        void Update(Book book);
        void Delete(int id);
    }
}
