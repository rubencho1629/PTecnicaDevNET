using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Books.Domain.Entities;
using Books.Domain.Interfaces;
using Books.Infrastructure.Persistence;

namespace Books.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BooksDbContext _context;

        public BookRepository(BooksDbContext context)
        {
            _context = context;
        }

        public int Count()
        {
            return _context.Books.Count();
        }

        public void Add(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void Update(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Books.Find(id);
            if (entity == null) return;

            _context.Books.Remove(entity);
            _context.SaveChanges();
        }

        public Book GetById(int id)
        {
            return _context.Books
                .Include(b => b.Author)
                .FirstOrDefault(b => b.Id == id);
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books
                .AsNoTracking()
                .Include(b => b.Author)
                .ToList();
        }
    }
}
