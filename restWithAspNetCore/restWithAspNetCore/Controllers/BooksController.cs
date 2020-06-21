using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using restWithAspNetCore.Businnes.Implementations;
using restWithAspNetCore.Model;

namespace restWithAspNetCore.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class BooksController : Controller
    {
        private IBookBusinnes _bookBusinnes;
        public BooksController(IBookBusinnes bookBusinnes)
        {
            _bookBusinnes = bookBusinnes;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_bookBusinnes.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var book = _bookBusinnes.FindById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            if (book == null) return BadRequest();
            return new ObjectResult(_bookBusinnes.Create(book));
        }

        [HttpPut]
        public IActionResult Put([FromBody] Book book)
        {
            var updateBook = _bookBusinnes.Update(book);
            if (book == null) return BadRequest();
            return new ObjectResult(updateBook);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _bookBusinnes.Delete(id);
            return NoContent();
        }
    }
}
