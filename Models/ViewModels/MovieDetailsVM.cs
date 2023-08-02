
namespace MoviesApplication.Models.ViewModels
{
    public class MovieDetailsVM
    {
        public MovieVM MovieVM { get; set; } = new ();
        public RatingVM RatingVM { get; set; } = new RatingVM();
        public CommentVM CommentVM { get; set; } = new CommentVM();  
    }
}
