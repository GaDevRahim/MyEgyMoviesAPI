using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyEgyMoviesAPI.Dtos;
using MyEgyMoviesAPI.Models;
using MyEgyMoviesAPI.Services;

namespace MyEgyMoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenresService _genresService;

        public GenresController(IGenresService genresService)
        {
            _genresService = genresService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            return Ok(await _genresService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenre(byte id)
        {
            var genre = await _genresService.GetById(id);

            if (genre == null)
                return NotFound($"Not Found That ID : {id}");

            return Ok(genre);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewGenre(GenreDto genreDto)
        {
            var genre = new Genre()
            {
                Name = genreDto.Name
            };

            await _genresService.Add(genre);

            return Ok(genre);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateData(byte id, [FromBody] GenreDto genreDto)
        {
            var genre = await _genresService.GetById(id);

            if (genre == null)
                return NotFound($"Not Found That ID : {id}");

            genre.Name = genreDto.Name;

            _genresService.Update(genre);
            return Ok(genre);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGenre(byte id)
        {
            var genre = await _genresService.GetById(id);

            if (genre == null)
                return NotFound($"Not Found That ID : {id}");

            _genresService.Delete(genre);

            return Ok(genre);
        }
    }
}
