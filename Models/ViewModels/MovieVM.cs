using MoviesApplication.Data.Interfaces;

namespace MoviesApplication.Models.ViewModels
{
    public class MovieVM
    {
        public Movie Movie { get; set; } =  new Movie();

        public List<Rating> Ratings { get; set; } = new List<Rating>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public double AverageRating
        {
            get
            {
                if (Ratings != null && Ratings.Count > 0)
                {
                    double sumOfRatings = Ratings.Sum(r => r.Stars ?? 0);
                    return sumOfRatings / Ratings.Count;
                }

                return 0;
            }
        }
    }
}
