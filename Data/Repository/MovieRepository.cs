using Microsoft.EntityFrameworkCore;
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
            var newMovie = await _context.Movies.AddAsync(movie);
            if (newMovie != null)
            {
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var movie =await _context.Movies.FindAsync(id);
            if(movie != null)
            {
                _context.Movies.Remove(movie);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Movie>> GetAllAsync() => await _context.Movies.ToListAsync();
         

        public async Task<Movie> GetByIdAsync(int id)
        {
           var movie =  await _context.Movies.FindAsync(id);
            return movie;
        }

        public bool UpdateAsync(Movie movie)
        {
            movie.Updated_at= DateTime.Now;
            var updatedMovie = _context.Update(movie);
            if(updatedMovie.GetType() == typeof(Movie))
                return true;
            return false;
        }
    }
}
