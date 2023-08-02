namespace MoviesApplication.Models.ViewModels
{
    public class RatingAndCommentVM
    {
        public Comment Comment { get; set; } = new Comment();
        public Rating Rating { get; set; } = new Rating();
        public int movieId { get; set; }
        public bool IsRated { get; set; }   
    }
}
