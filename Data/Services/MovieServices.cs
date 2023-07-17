using MoviesApplication.Data.Base;
using MoviesApplication.Data.Interfaces;
using MoviesApplication.Models;

namespace MoviesApplication.Data.Services
{
    public class MovieServices : EntityBaseRepository<Movie>, IMovieServices
    {
        public MovieServices(MoviesAppContext context) : base(context)
        {
        }
    }
}
