using System;
using System.ComponentModel.DataAnnotations;

namespace Books.Application.DTOs
{
    public class UpdateAuthorRequestDto
    {
        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        [StringLength(150)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "La ciudad de procedencia es obligatoria")]
        [StringLength(100)]
        public string City { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; }
    }
}
