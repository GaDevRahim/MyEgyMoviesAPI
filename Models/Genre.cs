﻿namespace MyEgyMoviesAPI.Models
{
    public class Genre
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
