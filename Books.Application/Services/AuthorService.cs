using System;
using System.Collections.Generic;
using System.Linq;
using Books.Application.DTOs;
using Books.Application.Interfaces;
using Books.Domain.Entities;
using Books.Domain.Exceptions;
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
            return author.Id;
        }

        public AuthorResponseDto GetById(int id)
        {
            var entity = _authorRepository.GetById(id);
            if (entity == null)
                throw new EntityNotFoundException("Autor no encontrado");

            return Map(entity);
        }

        public IEnumerable<AuthorResponseDto> GetAll()
        {
            return _authorRepository.GetAll()
                .Select(Map)
                .ToList();
        }

        public void Update(int id, UpdateAuthorRequestDto dto)
        {
            if (dto == null) throw new ArgumentNullException(nameof(dto));

            var entity = _authorRepository.GetById(id);
            if (entity == null)
                throw new EntityNotFoundException("Autor no encontrado");

            entity.FullName = dto.FullName?.Trim();
            entity.BirthDate = dto.BirthDate.Value;
            entity.City = dto.City?.Trim();
            entity.Email = dto.Email?.Trim();

            _authorRepository.Update(entity);
        }

        public void Delete(int id)
        {
            var entity = _authorRepository.GetById(id);
            if (entity == null)
                throw new EntityNotFoundException("Autor no encontrado");

            if (_authorRepository.HasBooks(id))
                throw new CannotDeleteAuthorWithBooksException();

            _authorRepository.Delete(id);
        }

        private static AuthorResponseDto Map(Author a)
        {
            return new AuthorResponseDto
            {
                Id = a.Id,
                FullName = a.FullName,
                BirthDate = a.BirthDate,
                City = a.City,
                Email = a.Email
            };
        }
    }
}
