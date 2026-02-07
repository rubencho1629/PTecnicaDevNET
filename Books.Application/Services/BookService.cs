using System;
using Books.Application.DTOs;
using Books.Application.Interfaces;
using Books.Application.Settings;
using Books.Domain.Entities;
using Books.Domain.Exceptions;
using Books.Domain.Interfaces;

namespace Books.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly BooksSettings _settings;

        public BookService(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            BooksSettings settings)
        {
            if (bookRepository == null) throw new ArgumentNullException(nameof(bookRepository));
            if (authorRepository == null) throw new ArgumentNullException(nameof(authorRepository));
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _settings = settings;
        }

        public int Create(CreateBookRequestDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            // Regla: el autor debe existir
            if (!_authorRepository.ExistsById(dto.AuthorId.Value))
                throw new AuthorNotRegisteredException();

            // Regla: máximo de libros permitido
            var currentCount = _bookRepository.Count();
            if (currentCount >= _settings.MaxBooksAllowed)
                throw new MaxBooksReachedException();

            var book = new Book
            {
                Title = dto.Title?.Trim(),
                Year = dto.Year.Value,
                Genre = dto.Genre?.Trim(),
                Pages = dto.Pages.Value,
                AuthorId = dto.AuthorId.Value
            };

            _bookRepository.Add(book);
            return book.Id;
        }
    }
}
