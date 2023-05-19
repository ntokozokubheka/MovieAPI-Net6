using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {

        private readonly ApplicationDBContext _dbContext;

        public MovieController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieModel>>> GetMovies()
        {
            if (_dbContext == null)
            {
                return NotFound();
            }

            return await _dbContext.Movies.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieModel>> GetMovie(int id)
        {
            if (_dbContext == null)
            {
                return NotFound();
            }
            var movie = await _dbContext.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        [HttpPost]
        public async Task<ActionResult<MovieModel>> PostMovie(MovieModel movie)
        {

            _dbContext.Movies.Add(movie);
            await _dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);


        }

        [HttpPut]
        public async Task<ActionResult> PutMovie(int id, MovieModel movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }
            _dbContext.Entry(movie).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();


        }
        private bool MovieExists(long id)
        {
            return (_dbContext.Movies?.Any(x => x.Id == id)).GetValueOrDefault();
        }
    }
}
