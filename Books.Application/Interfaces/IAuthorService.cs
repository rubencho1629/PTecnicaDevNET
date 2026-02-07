using System.Collections.Generic;
using Books.Application.DTOs;

namespace Books.Application.Interfaces
{
    public interface IAuthorService
    {
        int Create(CreateAuthorRequestDto dto);
        AuthorResponseDto GetById(int id);
        IEnumerable<AuthorResponseDto> GetAll();
        void Update(int id, UpdateAuthorRequestDto dto);
        void Delete(int id);
    }
}
