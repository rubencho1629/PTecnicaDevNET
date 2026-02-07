using System;

namespace Books.Domain.Exceptions
{
    public class CannotDeleteAuthorWithBooksException : Exception
    {
        public CannotDeleteAuthorWithBooksException()
            : base("No es posible eliminar el autor porque tiene libros asociados.")
        {
        }
    }
}
