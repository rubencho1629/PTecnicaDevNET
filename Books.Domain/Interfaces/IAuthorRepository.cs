using System.Collections.Generic;
using Books.Domain.Entities;

namespace Books.Domain.Interfaces
{
    public interface IAuthorRepository
    {
        Author GetById(int id);
        bool ExistsById(int id);
        IEnumerable<Author> GetAll();
        void Add(Author author);
        void Update(Author author);
        void Delete(int id);


        bool HasBooks(int authorId);
    }
}
