using System.ComponentModel.DataAnnotations;

namespace Books.Application.DTOs
{
    public class UpdateBookRequestDto
    {
        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(200)]
        public string Title { get; set; }

        [Required(ErrorMessage = "El año es obligatorio")]
        [Range(1, 9999)]
        public int? Year { get; set; }

        [Required(ErrorMessage = "El género es obligatorio")]
        [StringLength(80)]
        public string Genre { get; set; }

        [Required(ErrorMessage = "El número de páginas es obligatorio")]
        [Range(1, 200000)]
        public int? Pages { get; set; }

        [Required(ErrorMessage = "El autor es obligatorio")]
        [Range(1, int.MaxValue)]
        public int? AuthorId { get; set; }
    }
}
