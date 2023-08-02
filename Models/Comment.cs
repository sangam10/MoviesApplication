using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesApplication.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }

        // Navigation properties
        [ForeignKey("MovieId")]
        public int MovieId { get; set; }
        public Movie ? Movie { get; set; }

        [ForeignKey("ApplicationUserId")]
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? CommentBy { get; set; }
    }
}
