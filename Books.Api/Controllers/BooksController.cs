using System;
using System.Web.Http;
using Books.Application.DTOs;
using Books.Application.Interfaces;
using Books.Domain.Exceptions;

namespace Books.Api.Controllers
{
    [RoutePrefix("api/books")]
    public class BooksController : ApiController
    {
        private readonly IBookService _service;

        public BooksController(IBookService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(CreateBookRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var id = _service.Create(dto);
                return Ok(id);
            }
            catch (AuthorNotRegisteredException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (MaxBooksReachedException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
