namespace MoviesApplication.Models.ViewModels
{
    public class RatingVM
    {
        public Rating Rating { get; set; } = new Rating();
        public int MovieId { get; set; }    
    }
}
