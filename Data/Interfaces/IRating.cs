using MoviesApplication.Models;

namespace MoviesApplication.Data.Interfaces
{
    public interface IRating
    {
        Task<Rating> GetByIdAsync(int? id);
        Task<bool> AddAsync(Rating movie);
        Task<bool> UpdateAsync(Rating movie);
        Task<bool> DeleteAsync(Rating movie);

        bool HasRated(int movieId,string? ApplicationUserId);
        Task<Rating> FindRatingToMovieUser(int movieId,string ApplicationUserId);
    }
}
