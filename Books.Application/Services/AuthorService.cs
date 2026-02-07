using System;
using Books.Application.DTOs;
using Books.Application.Interfaces;
using Books.Domain.Entities;
using Books.Domain.Interfaces;

namespace Books.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            if (authorRepository == null) throw new ArgumentNullException(nameof(authorRepository));
            _authorRepository = authorRepository;
        }

        public int Create(CreateAuthorRequestDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var author = new Author
            {
                FullName = dto.FullName?.Trim(),
                BirthDate = dto.BirthDate.Value,
                City = dto.City?.Trim(),
                Email = dto.Email?.Trim()
            };

            _authorRepository.Add(author);

            // EF asignará el Id al guardar (en Infrastructure)
            return author.Id;
        }
    }
}
