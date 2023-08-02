
namespace MoviesApplication.Models.ViewModels
{
    public class MovieDetailsVM
    {
        public Movie Movie { get; set; } = new Movie();
        public List<Comment>? Comments { get; set; }
        public List<Rating>? Ratings { get; set; }
        /*public RatingAndCommentVM RatingAndCommentVM {get; set; } = new RatingAndCommentVM();*/
        public RatingVM RatingVM { get; set; } = new RatingVM();
        public CommentVM CommentVM { get; set; } = new CommentVM();  
        public double AverageRating
        {
            get
            {
                if (Ratings != null && Ratings.Count > 0)
                {
                    // Calculate the average of all ratings
                    double sumOfRatings = Ratings.Sum(r => r.Stars ?? 0);
                    return sumOfRatings / Ratings.Count;
                }

                return 0; // Default average rating when there are no ratings
            }
        }
    }
}
