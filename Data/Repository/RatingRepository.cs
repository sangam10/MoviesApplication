using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using MoviesApplication.Data.Interfaces;
using MoviesApplication.Models;

namespace MoviesApplication.Data.Repository
{
    public class RatingRepository : IRating
    {
        private readonly MoviesAppContext _context;
        public RatingRepository(MoviesAppContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(Rating rating)
        {
            await _context.Ratings.AddAsync(rating);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Rating rating)
        {
            _context.Ratings.Remove(rating);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Rating> GetByIdAsync(int? id)
        {
            return await _context.Ratings.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<bool> UpdateAsync(Rating rating)
        {
            _context.Ratings.Update(rating);
            await _context.SaveChangesAsync();
            return true;
        }
        public bool HasRated(int movieId,string? ApplicationUserId)
        {
            bool hasRated = _context.Ratings
                .Any(r => r.MovieId == movieId && r.ApplicationUserId == ApplicationUserId);
            return hasRated;
        }

        public async Task<Rating> FindRatingToMovieUser(int movieId, string ApplicationUserId)
        {
            var rating = await _context.Ratings.FirstOrDefaultAsync(r=>r.ApplicationUserId == ApplicationUserId && r.MovieId == movieId);
            return rating;
        }
    }
}
