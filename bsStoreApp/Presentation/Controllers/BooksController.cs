using Entities.DataTransferObject;
using Entities.Exceptions;
using Entities.Models;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.ActionFilters;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ServiceFilter(typeof(LogFilterAttribute))]
    [ApiController]
    [Route("api/books")]
    //[ResponseCache(CacheProfileName ="5mins")]
    //[HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 80)]
    public class BooksController : ControllerBase
    {

        private readonly IServiceManager _manager;

        public BooksController(IServiceManager manager)
        {
            _manager = manager;
        }

        [Authorize(Roles = "User")]
        [HttpHead]
        [HttpGet]
        //[ResponseCache(Duration = 60)]
        public async Task<IActionResult> GetAllBookAsync()
        {
            var books = await _manager.BookService.GetAllBooksAsync(false);
            return Ok(books);
        }

        [Authorize(Roles = "Editor")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneBookAsync(int id)
        {
            
            var book = await _manager.BookService.GetOneBookByIdAsync(id, false);

            if (book is null)
                throw new BookNotFoundException(id);
            return Ok(book);
        }

        [Authorize(Roles = "Admin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPost]
        public async Task<IActionResult> CreateOneBookAsync(BookDtoForInsertion bookDto)   
        {           
            var book = await _manager.BookService.CreateOneBookAsync(bookDto);

            return StatusCode(201, book);
        }

        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOneBookAsync(int id, BookDtoForUpdate bookDto)
        {   
            await _manager.BookService.UpdateOneBookAsync(id, bookDto, false);

            return NoContent(); //204
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookAsync(int id)
        {
            await _manager.BookService.DeleteOneBookAsync(id, false);
            return NoContent();
        }

        [HttpOptions]
        public IActionResult GetBooksOptions()
        {
            Response.Headers.Add("Allow", "GET, PUT, POST, DELETE, HEAD, OPTIONS");
            return Ok();        
        }

    }
}
