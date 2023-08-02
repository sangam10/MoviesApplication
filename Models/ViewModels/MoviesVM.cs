namespace MoviesApplication.Models.ViewModels
{
    public class MoviesVM
    {
        public List<MovieVM> MovieVMS { get; set; } = new ();
        public string? MovieName { get; set; }
    }
}
