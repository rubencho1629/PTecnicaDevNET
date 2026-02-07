using System.ComponentModel.DataAnnotations;

namespace Books.Application.DTOs
{
    public class UpdateBookRequestDto
    {
        [Required, StringLength(200)]
        public string Title { get; set; }

        [Required, Range(1, 9999)]
        public int? Year { get; set; }

        [Required, StringLength(80)]
        public string Genre { get; set; }

        [Required, Range(1, 200000)]
        public int? Pages { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int? AuthorId { get; set; }
    }
}
