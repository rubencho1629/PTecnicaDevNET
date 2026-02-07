using Books.Domain.Entities;

namespace Books.Domain.Interfaces
{
    public interface IBookRepository
    {
        int Count();
        void Add(Book book);
    }
}
