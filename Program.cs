using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoviesApplication.Data;
using MoviesApplication.Data.Interfaces;
using MoviesApplication.Data.Repository;
using MoviesApplication.Data.Services;
using MoviesApplication.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();

builder.Services.AddDbContext<MoviesAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MoviesConnString")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddDefaultTokenProviders()
        .AddEntityFrameworkStores<MoviesAppContext>();

//service constainer
builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IRating, RatingRepository>();
builder.Services.AddScoped<IComment, CommentRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Movies}/{action=Index}/{id?}");

//seed roles
DataSeeder.UserRolesSeed(app);
DataSeeder.seed(app);

app.Run();
