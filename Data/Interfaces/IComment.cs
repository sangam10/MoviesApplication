using MoviesApplication.Models;

namespace MoviesApplication.Data.Interfaces
{
    public interface IComment
    {
        Task<bool> AddAsync(Comment comment);
    }
}
