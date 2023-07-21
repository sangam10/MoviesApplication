using MoviesApplication.Data.Base;
using MoviesApplication.Models;

namespace MoviesApplication.Data.Interfaces
{
    public interface IMovieRepository
    {
        Task<IEnumerable<Movie>> GetAllAsync();
        Task<Movie> GetByIdAsync(int id);
        Task<bool> AddAsync(Movie movie);
        bool UpdateAsync(Movie movie);
        Task<bool> DeleteAsync(int id);
    }
}
