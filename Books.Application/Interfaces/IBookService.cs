using Books.Application.DTOs;

namespace Books.Application.Interfaces
{
    public interface IBookService
    {
        int Create(CreateBookRequestDto dto);
    }
}
