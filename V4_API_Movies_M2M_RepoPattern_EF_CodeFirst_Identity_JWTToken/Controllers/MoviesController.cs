using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.Data;
using V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.dto;
using V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.Models;

namespace V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
       IRepository repository;
        public MoviesController(IRepository repository)
        {
            this.repository = repository;
        }

        // GET: api/Movies
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Movies/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Movies
        [HttpPost]
        public IActionResult Post([FromBody] MovieDto movie)
        {
            if (ModelState.IsValid)
            {
                bool result = repository.AddMovie(movie);
                if (result)
                {
                    return Created("AddMovie", movie);
                }
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return BadRequest(ModelState);
        }
        // PUT: api/Movies/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] MovieDto movie)
        {
            if (ModelState.IsValid && id == movie.Movie.Id)
            {
                bool result = repository.UpdateMovie(movie);
                if (result)
                {
                    return Ok();
                }
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
            return BadRequest(ModelState);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var movie = repository.GetMovie(id);
            if (movie == null)
            {
                return NotFound();
            }
            bool result = repository.DeleteMovie(movie);
            if (result)
            {
                return Ok();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
