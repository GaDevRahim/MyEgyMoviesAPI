namespace MyEgyMoviesAPI.Services
{
    public interface IMoviesService
    {
        Task<IEnumerable<Movie>> GetAll();
        Task<Movie> GetByID(int id);
        Task<Movie> Add(Movie movie);
        Movie Update(Movie movie);

        Movie Delete(Movie movie);
    }
}
