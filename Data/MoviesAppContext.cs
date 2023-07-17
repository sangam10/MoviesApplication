
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoviesApplication.Models;

namespace MoviesApplication.Data
{
    public class MoviesAppContext : IdentityDbContext<ApplicationUser>
    {
        public MoviesAppContext(DbContextOptions<MoviesAppContext>options):base(options) { }
        public DbSet<Movie> Movies { get; set; }
    }

}