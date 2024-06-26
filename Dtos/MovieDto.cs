﻿namespace MyEgyMoviesAPI.Dtos
{
    public class MovieDto
    {

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;

        [Range(1950, 2024)]
        public int Year { get; set; }

        public double Rate { get; set; }

        public IFormFile? Poster { get; set; }

        public byte GenreId { get; set; }

    }
}
