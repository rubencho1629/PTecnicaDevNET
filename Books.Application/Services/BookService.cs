using System;
using System.Collections.Generic;
using System.Linq;
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

        // CREATE
        public int Create(CreateBookRequestDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            // Regla: autor debe existir
            if (!_authorRepository.ExistsById(dto.AuthorId.Value))
                throw new AuthorNotRegisteredException();

            // Regla: máximo permitido
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

        // READ ALL
        public IEnumerable<BookResponseDto> GetAll()
        {
            return _bookRepository.GetAll()
                .Select(Map)
                .ToList();
        }

        // READ BY ID
        public BookResponseDto GetById(int id)
        {
            var entity = _bookRepository.GetById(id);
            if (entity == null)
                throw new EntityNotFoundException("Libro no encontrado");

            return Map(entity);
        }

        // UPDATE
        public void Update(int id, UpdateBookRequestDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var entity = _bookRepository.GetById(id);
            if (entity == null)
                throw new EntityNotFoundException("Libro no encontrado");

            // Regla: autor debe existir
            if (!_authorRepository.ExistsById(dto.AuthorId.Value))
                throw new AuthorNotRegisteredException();

            entity.Title = dto.Title?.Trim();
            entity.Year = dto.Year.Value;
            entity.Genre = dto.Genre?.Trim();
            entity.Pages = dto.Pages.Value;
            entity.AuthorId = dto.AuthorId.Value;

            _bookRepository.Update(entity);
        }

        // DELETE
        public void Delete(int id)
        {
            var entity = _bookRepository.GetById(id);
            if (entity == null)
                throw new EntityNotFoundException("Libro no encontrado");

            _bookRepository.Delete(id);
        }

        private static BookResponseDto Map(Book b)
        {
            return new BookResponseDto
            {
                Id = b.Id,
                Title = b.Title,
                Year = b.Year,
                Genre = b.Genre,
                Pages = b.Pages,
                AuthorId = b.AuthorId,
                AuthorName = b.Author != null ? b.Author.FullName : null
            };
        }
    }
}
