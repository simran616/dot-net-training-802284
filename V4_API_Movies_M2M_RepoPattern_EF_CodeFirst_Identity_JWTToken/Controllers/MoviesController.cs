using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.Data;
using V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.dto;
using V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.Models;

namespace V4_API_Movies_M2M_RepoPattern_EF_CodeFirst_Identity_JWTToken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MoviesController : ControllerBase
    {
       IRepository repository;
        public MoviesController(IRepository repository)
        {
            this.repository = repository;
        }


        // GET: api/Movies
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(repository.GetMovies());
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


        [HttpGet("actor/{id}")]
        //GET api/movies/actor/1
        //GET api/movies/actor?id=1
        public IActionResult MoviesByActor(int id)
        {
            var movies = repository.GetMoviesByActor(id);
            if (!movies.Any())
            {
                return NoContent();
            }
            return Ok(movies);
        }
        
        
        [HttpGet("genre/{id}")]
        //GET api/movies/genre/1
        //GET api/movies/genre?id=1
        public IActionResult MoviesByGenre(int id)
        {

            {
                bool isvalid = Enum.IsDefined(typeof(Genre), id);
                if (!isvalid)
                {
                    return BadRequest("Invalid genre");
                }
                var movies = repository.GetMoviesByGenre((Genre)id);
                if (!movies.Any())
                {
                    return NoContent();
                }
                return Ok(movies);
            }
        }
    }
}
