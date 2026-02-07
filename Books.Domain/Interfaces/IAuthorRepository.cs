using Books.Domain.Entities;

namespace Books.Domain.Interfaces
{
    public interface IAuthorRepository
    {
        Author GetById(int id);
        bool ExistsById(int id);
        void Add(Author author);
    }
}
