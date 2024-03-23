using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyEgyMoviesAPI.Dtos;
using MyEgyMoviesAPI.Models;

namespace MyEgyMoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            return Ok(await _context.Genres.ToArrayAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddNewGenre(GenreDto genreDto)
        {
            var genra = new Genre()
            {
                Name = genreDto.Name
            };

            await _context.Genres.AddAsync(genra);
            _context.SaveChanges();

            return Ok(genra);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenre(int id)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);

            if (genre == null)
                return NotFound($"Not Found That ID : {id}");

            return Ok(genre);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);

            if (genre == null)
                return NotFound($"Not Found That ID : {id}");

            _context.Genres.Remove(genre);
            _context.SaveChanges();

            return Ok(genre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateData(int id, [FromBody] GenreDto genreDto)
        {
            var genre = await _context.Genres.SingleOrDefaultAsync(g => g.Id == id);

            if (genre == null)
                return NotFound($"Not Found That ID : {id}");

            genre.Name = genreDto.Name;

            _context.SaveChanges();
            return Ok(genre);
        }
    }
}
