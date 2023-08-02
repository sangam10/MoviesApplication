using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesApplication.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Movie Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Poster Image")]
        public string Poster_Image { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Movie Description")]
        public string Short_Desc { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Release Date")]
        public DateTime Release_Date { get; set; }
        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime Updated_at { get; set; } = DateTime.Now;

        //relationship
        public string ? ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser? Creator { get; set; }

        //movies has many comments
        public ICollection<Rating> ? Ratings { get; set; }
        public ICollection<Comment> ? Comments { get; set; }
    }
}
    