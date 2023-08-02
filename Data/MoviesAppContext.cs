
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoviesApplication.Models;

namespace MoviesApplication.Data
{
    public class MoviesAppContext : IdentityDbContext<ApplicationUser>
    {
        public MoviesAppContext(DbContextOptions<MoviesAppContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Rating>()
                .HasIndex(r => new { r.ApplicationUserId, r.MovieId })
                .IsUnique()
                .HasDatabaseName("IX_Unique_UserId_MovieId");
            base.OnModelCreating(builder);
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }

}