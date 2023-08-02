
namespace MoviesApplication.Models.ViewModels
{
    public class MovieDetailsVM
    {
       /* public Movie Movie { get; set; } = new Movie();
        public List<Comment>? Comments { get; set; }
        public List<Rating>? Ratings { get; set; }*/
        public MovieVM MovieVM { get; set; } = new ();
        public RatingVM RatingVM { get; set; } = new RatingVM();
        public CommentVM CommentVM { get; set; } = new CommentVM();  
    }
}
