using MoviesApplication.Data.Interfaces;
using MoviesApplication.Models;

namespace MoviesApplication.Data.Repository
{
    public class CommentRepository : IComment
    {
        private readonly MoviesAppContext _context;
        public CommentRepository(MoviesAppContext context)
        {
            _context = context;
        }
        public async Task<bool> AddAsync(Comment comment)
        {
            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
