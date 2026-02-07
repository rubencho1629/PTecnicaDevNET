using System;
using System.Collections.Generic;

namespace Books.Domain.Entities
{
    public class Author
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public DateTime BirthDate { get; set; }

        public string City { get; set; }

        public string Email { get; set; }

       
        public virtual ICollection<Book> Books { get; set; }

        public Author()
        {
            Books = new List<Book>();
        }
    }
}
