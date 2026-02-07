using System;
using System.ComponentModel.DataAnnotations;

namespace Books.Front.Models
{
    public class AuthorVm
    {
        public int Id { get; set; }

        [Required, StringLength(150)]
        [Display(Name = "Nombre completo")]
        public string FullName { get; set; }

        [Required]
        [Display(Name = "Fecha de nacimiento")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required, StringLength(100)]
        [Display(Name = "Ciudad")]
        public string City { get; set; }

        [Required, EmailAddress, StringLength(150)]
        [Display(Name = "Correo")]
        public string Email { get; set; }
    }
}
