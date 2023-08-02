using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesApplication.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int? Stars { get; set; }
        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime Updated_At { get; set; } = DateTime.Now;

        // Relationship with the Movie model
        [ForeignKey("MovieId")]
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }

        // Relation with user
        [ForeignKey("ApplicationUserId")]
        public string? ApplicationUserId { get; set; }
        public ApplicationUser? RatedBy { get; set; }
    }
}

