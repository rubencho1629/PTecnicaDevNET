using System;

namespace Books.Application.DTOs
{
    public class AuthorResponseDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
    }
}
