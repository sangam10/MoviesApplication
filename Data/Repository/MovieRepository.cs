using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MoviesApplication.Data.Base;
using MoviesApplication.Data.Interfaces;
using MoviesApplication.Models;

namespace MoviesApplication.Data.Services
{
    public class MovieRepository : IMovieRepository
    {
        private readonly MoviesAppContext _context;
        public MovieRepository(MoviesAppContext context)
        {
            _context= context;
        }

        public async Task<bool> AddAsync(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Movie movie)
        {
            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Movie>> FindMoviesByName(string? name)
        {
            var movies =  await _context.Movies.Where(m=>m.Name.Contains(name)).ToListAsync();
            return movies;
        }

        public async Task<ICollection<Movie>> GetAllAsync()
        {
           return await _context.Movies.Include(m => m.Ratings).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            return await _context.Movies.Include(m => m.Creator)
                .Include(m=>m.Ratings)
                .FirstOrDefaultAsync(m => m.Id == id) ?? new Movie();
        }

        public async Task<Movie> GetByIdWithRatingsAndCommentsAsync(int id)
        {
            return await _context.Movies.Include(m => m.Creator)
                .Include(m => m.Ratings)
                .Include(m => m.Comments)
                .FirstOrDefaultAsync(m => m.Id == id) ?? new Movie();
        }

        public async Task<bool> UpdateAsync(Movie movie)
        {
            _context.Movies.Update(movie);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
