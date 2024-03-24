using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyEgyMoviesAPI.Dtos;
using MyEgyMoviesAPI.Models;
using System.Reflection.Metadata;

namespace MyEgyMoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private List<string> allowsExten = new List<string> { ".png", ".jpg" };
        private int allowMaxSize = 1048576;

        public MoviesController(ApplicationDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMovies()
        {
            
            var movies = await _context.Movies.Include(m => m.Genre).ToListAsync();
            
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieDetials(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(x => x.Id == id);

            if (movie == null)
                return NotFound($"Not Found That ID : {id}");

            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewMovie([FromForm] MovieDto movieDto)
        {
            if (movieDto.Poster == null)
                return BadRequest("Poster is required!");

            if (!allowsExten.Contains(Path.GetExtension(movieDto.Poster.FileName).ToLower()))
                return BadRequest("Only .png and .jpg images are allowed!");

            if (movieDto.Poster.Length > allowMaxSize)
                return BadRequest("Max allowed size for poster is 1MB!");

            bool isCorrectGenreId = await _context.Genres.AnyAsync(g => g.Id == movieDto.GenreId);

            if (!isCorrectGenreId)
                return BadRequest($"Invalid genere ID! {isCorrectGenreId}");

            using var stream = new MemoryStream();
            await movieDto.Poster.CopyToAsync(stream);

            var movie = new Movie
            {
                Name = movieDto.Name,
                Year = movieDto.Year,
                Description = movieDto.Description,
                GenreId = movieDto.GenreId,
                Rate = movieDto.Rate,
                Poster = stream.ToArray(),
            };

            await _context.AddAsync(movie);
            _context.SaveChanges();
            return Ok(movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovieData(int id, [FromForm] MovieDto movieDto)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
                return NotFound($"No movie was found with ID {id}");

            bool isCorrectGenreId = await _context.Genres.AnyAsync(g => g.Id == movieDto.GenreId);
            if (!isCorrectGenreId)
                return BadRequest($"Invalid genere ID! {isCorrectGenreId}");

            if (movieDto.Poster != null)
            {
                if (!allowsExten.Contains(Path.GetExtension(movieDto.Poster.FileName).ToLower()))
                    return BadRequest("Only .png and .jpg images are allowed!");

                if (movieDto.Poster.Length > allowMaxSize)
                    return BadRequest("Max allowed size for poster is 1MB!");

                using var stream = new MemoryStream();
                await movieDto.Poster.CopyToAsync(stream);

                movie.Poster = stream.ToArray();
            }
            
            movie.Name = movieDto.Name;
            movie.Description = movieDto.Description;
            movie.Year = movieDto.Year;
            movie.Rate = movieDto.Rate;
            movie.GenreId = movieDto.GenreId;

            _context.SaveChanges();
            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
            if (movie == null)
                return NotFound($"No movie was found with ID {id}");

            _context.Movies.Remove(movie);
            _context.SaveChanges();

            return Ok(movie);
        }
    }
}
