namespace MoviesApplication.Models.ViewModels
{
    public class MoviesVM
    {
        public List<Movie> Movies { get; set; } = new ();
        public string? MovieName { get; set; }
    }
}
