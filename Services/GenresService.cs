using MyEgyMoviesAPI.Services;
namespace MyEgyMoviesAPI.Services
{
    public class GenresService : IGenresService
    {
        private readonly ApplicationDbContext _context;

        public GenresService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Genre> Add(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            _context.SaveChanges();

            return genre;
        }

        public Genre Delete(Genre genre)
        {
            _context.Genres.Remove(genre);
            _context.SaveChanges();

            return genre;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await _context.Genres.OrderBy(g => g.Name).ToArrayAsync();
        }

        public async Task<Genre> GetById(byte id)
        {
            return await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);
        }

        public Genre Update(Genre genre)
        {
            _context.Genres.Update(genre);
            _context.SaveChanges();

            return genre;
        }
        public async Task<bool> isCorrectId(byte id)
        {
            return await _context.Genres.AnyAsync(g => g.Id == id);
        }
    }
}
