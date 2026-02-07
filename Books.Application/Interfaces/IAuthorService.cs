using Books.Application.DTOs;

namespace Books.Application.Interfaces
{
    public interface IAuthorService
    {
        int Create(CreateAuthorRequestDto dto);
    }
}
