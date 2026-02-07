using System.Collections.Generic;
using Books.Application.DTOs;

namespace Books.Application.Interfaces
{
    public interface IBookService
    {
        int Create(CreateBookRequestDto dto);

        IEnumerable<BookResponseDto> GetAll();
        BookResponseDto GetById(int id);

        void Update(int id, UpdateBookRequestDto dto);
        void Delete(int id);
    }
}
