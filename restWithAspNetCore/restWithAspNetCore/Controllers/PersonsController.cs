using Microsoft.AspNetCore.Mvc;
using restWithAspNetCore.Model;
using restWithAspNetCore.Businnes.Implementations;
using restWithAspNetCore.Data.VO;
using Tapioca.HATEOAS;

namespace restWithAspNetCore.Controllers
{
    
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonsController : Controller
    {
        private IPersonBusinnes _personBusinnes;

        public PersonsController(IPersonBusinnes personBusinnes)
        {
            _personBusinnes = personBusinnes;
        }

        // GET api/values
        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get()
        {
            return Ok(_personBusinnes.FindAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Get(int id)
        {
            var person = _personBusinnes.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        // POST api/values
        [HttpPost]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Post([FromBody] PersonVO person)
        {
            if (person == null) return BadRequest();
            return new ObjectResult(_personBusinnes.Create(person));
        }

         
        [HttpPut]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Put([FromBody] PersonVO person)
        {
            var updatePerson = _personBusinnes.Update(person);
            if (person == null) return BadRequest();
            return new ObjectResult(updatePerson);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult Delete(int id)
        {
            _personBusinnes.Delete(id);
            return NoContent();
        }
    }
}
