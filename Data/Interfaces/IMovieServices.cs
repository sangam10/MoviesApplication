using MoviesApplication.Data.Base;
using MoviesApplication.Models;

namespace MoviesApplication.Data.Interfaces
{
    public interface IMovieServices : IEntityBaseRepository<Movie>
    {
    }
}
