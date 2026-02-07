using System.ComponentModel.DataAnnotations;

namespace Books.Front.Models
{
    public class BookVm
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        [Display(Name = "Título")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Año")]
        [Range(1, 9999)]
        public int? Year { get; set; }

        [Required, StringLength(80)]
        [Display(Name = "Género")]
        public string Genre { get; set; }

        [Required]
        [Display(Name = "Páginas")]
        [Range(1, 200000)]
        public int? Pages { get; set; }

        [Required]
        [Display(Name = "Autor")]
        public int? AuthorId { get; set; }

        // Para mostrar en tabla
        public string AuthorName { get; set; }
    }
}
