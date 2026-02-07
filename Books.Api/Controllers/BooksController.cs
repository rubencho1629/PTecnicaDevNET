using System.Net;
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

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                return Ok(_service.GetById(id));
            }
            catch (EntityNotFoundException ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
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
                return Content(HttpStatusCode.Created, new { id });
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

        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Update(int id, UpdateBookRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _service.Update(id, dto);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (EntityNotFoundException ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
            catch (AuthorNotRegisteredException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (EntityNotFoundException ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }
    }
}
