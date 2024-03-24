
namespace MyEgyMoviesAPI.Models
{
    public class Movie
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;

        [Range(1950, 2024)]
        public int Year { get; set; }

        public double Rate { get; set; }

        [Required]
        public byte[] Poster { get; set; }

        public  byte GenreId { get; set; }
  
        public Genre Genre { get; set; }
    }
}
