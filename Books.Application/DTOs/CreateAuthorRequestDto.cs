using System;
using System.ComponentModel.DataAnnotations;

namespace Books.Application.DTOs
{
    public class CreateAuthorRequestDto
    {
        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        [StringLength(150, ErrorMessage = "El nombre completo no puede superar 150 caracteres")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "La ciudad de procedencia es obligatoria")]
        [StringLength(100, ErrorMessage = "La ciudad no puede superar 100 caracteres")]
        public string City { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo electrónico no tiene un formato válido")]
        [StringLength(150, ErrorMessage = "El correo no puede superar 150 caracteres")]
        public string Email { get; set; }
    }
}
