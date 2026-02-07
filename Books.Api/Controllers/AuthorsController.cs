using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Http;
using Books.Application.DTOs;
using Books.Application.Interfaces;
using Books.Domain.Exceptions;

namespace Books.Api.Controllers
{
    [RoutePrefix("api/authors")]
    public class AuthorsController : ApiController
    {
        private readonly IAuthorService _service;

        public AuthorsController(IAuthorService service)
        {
            _service = service;
        }

   
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var data = _service.GetAll();
            return Ok(data);
        }
   
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                var data = _service.GetById(id);
                return Ok(data);
            }
            catch (EntityNotFoundException ex)
            {
                return Content(HttpStatusCode.NotFound, ex.Message);
            }
        }
      
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(CreateAuthorRequestDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var id = _service.Create(dto);
                return Content(HttpStatusCode.Created, new { id });
            }
            catch (DbUpdateException)
            {
               
                return BadRequest("Ya existe un autor con ese correo electrónico.");
            }
        }

   
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Update(int id, UpdateAuthorRequestDto dto)
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
            catch (DbUpdateException)
            {
                return BadRequest("Ya existe un autor con ese correo electrónico.");
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
            catch (CannotDeleteAuthorWithBooksException ex)
            {
                return Content(HttpStatusCode.Conflict, ex.Message);
            }
        }
    }
}
