using System.ComponentModel.DataAnnotations;

namespace Books.Application.DTOs
{
    public class CreateBookRequestDto
    {
        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(200, ErrorMessage = "El título no puede superar 200 caracteres")]
        public string Title { get; set; }

        [Required(ErrorMessage = "El año es obligatorio")]
        [Range(1, 9999, ErrorMessage = "El año debe ser válido")]
        public int? Year { get; set; }

        [Required(ErrorMessage = "El género es obligatorio")]
        [StringLength(80, ErrorMessage = "El género no puede superar 80 caracteres")]
        public string Genre { get; set; }

        [Required(ErrorMessage = "El número de páginas es obligatorio")]
        [Range(1, 200000, ErrorMessage = "El número de páginas debe ser mayor que 0")]
        public int? Pages { get; set; }

        [Required(ErrorMessage = "El autor es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El autor es obligatorio")]
        public int? AuthorId { get; set; }
    }
}
