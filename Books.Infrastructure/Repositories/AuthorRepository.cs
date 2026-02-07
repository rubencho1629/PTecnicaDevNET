using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Books.Domain.Entities;
using Books.Domain.Interfaces;
using Books.Infrastructure.Persistence;

namespace Books.Infrastructure.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BooksDbContext _context;

        public AuthorRepository(BooksDbContext context)
        {
            _context = context;
        }

        public Author GetById(int id)
        {
            return _context.Authors.Find(id);
        }

        public bool ExistsById(int id)
        {
            return _context.Authors.Any(a => a.Id == id);
        }

        public IEnumerable<Author> GetAll()
        {
            return _context.Authors.AsNoTracking().ToList();
        }

        public void Add(Author author)
        {
            _context.Authors.Add(author);
            _context.SaveChanges();
        }

        public void Update(Author author)
        {
            _context.Entry(author).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Authors.Find(id);
            if (entity == null) return;

            _context.Authors.Remove(entity);
            _context.SaveChanges();
        }

        public bool HasBooks(int authorId)
        {
            return _context.Books.Any(b => b.AuthorId == authorId);
        }
    }
}
