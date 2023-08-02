using MoviesApplication.Data.Base;
using MoviesApplication.Models;

namespace MoviesApplication.Data.Interfaces
{
    public interface IMovieRepository
    { 
        Task<ICollection<Movie>> GetAllAsync();
        Task<Movie> GetByIdAsync(int id);
        Task<Movie> GetByIdWithRatingsAndCommentsAsync(int id);
        Task<bool> AddAsync(Movie movie);
        Task<bool> UpdateAsync(Movie movie);
        Task<bool> DeleteAsync(Movie movie);
        Task<IEnumerable<Movie>> FindMoviesByName(string name);
    }
}
