
using MoviesApplication.Data;
using MoviesApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using MoviesApplication.Data.Static;
using MoviesApplication.Data.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MoviesApplication.Models.ViewModels;
using MoviesApplication.Data.Repository;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

namespace MoviesApplication.Controllers;

public class MoviesController : Controller
{
    private readonly MoviesAppContext _context;
    private readonly IMovieRepository _movieRepository;
    private readonly IRating _rating;

    public MoviesController(MoviesAppContext context, IMovieRepository movieRepository, IRating rating)
    {
        _context = context;
        _movieRepository = movieRepository;
        _rating = rating;
    }
    public async Task<IActionResult> Index(MoviesVM vm)
    {
        MoviesVM moviesVM = new();
        if (!vm.MovieName.IsNullOrEmpty())
        {
            var movies = await _movieRepository.FindMoviesByName(vm.MovieName);

            moviesVM.MovieVMS = movies.Select(movie => new MovieVM()
            {
                Movie = movie,
                Ratings = movie.Ratings != null ? movie.Ratings.ToList() : new List<Rating>()
            }).ToList();

            moviesVM.MovieName = vm.MovieName;

            return View(moviesVM);
        }
        else
        {
            var movies = await _movieRepository.GetAllAsync();

            moviesVM.MovieVMS = movies.Select(movie => new MovieVM()
            {
                Movie = movie,
                Ratings = movie.Ratings != null ? movie.Ratings.ToList() : new List<Rating>()
            }).ToList();
           
            return View(moviesVM);
        }
    }
    [Authorize(Roles = UserRoles.ADMIN)]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize(Roles = UserRoles.ADMIN)]
    public async Task<IActionResult> Edit(int id)
    {
        var movie = await _movieRepository.GetByIdAsync(id);
        if (movie == null)
            return NotFound();
        Console.WriteLine(movie?.Creator?.UserName);
        return View(movie);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = UserRoles.ADMIN)]
    public async Task<IActionResult> Create(Movie movie)
    {
        if (ModelState.IsValid)
        {
            try
            {
                movie.ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _movieRepository.AddAsync(movie);
                TempData["success"] = "Movie Created Successfully!!";
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.ToString();
                return View(movie);
            }
        }
        else
        {
            TempData["error"] = "Failed to Create!!";
            return View(movie);
        }
        return RedirectToAction(nameof(Index));
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = UserRoles.ADMIN)]
    public async Task<IActionResult> Delete(int id)
    {
        Console.WriteLine("movie delete");
        var movie = await _movieRepository.GetByIdAsync(id);
        if (movie == null)
        {
            return NotFound();
        }
        try
        {
            await _movieRepository.DeleteAsync(movie);
            await _context.SaveChangesAsync();
            TempData["success"] = "Movie deleted Successfully!!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception e)
        {
            Console.Write("error is =>" + e);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = UserRoles.ADMIN)]
    public async Task<IActionResult> Update(int id, Movie movie)
    {

        if (id != movie.Id)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            try
            {
                movie.ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                await _movieRepository.UpdateAsync(movie);
                TempData["success"] = "successfully updated";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException e)
            {
                Console.WriteLine("error is :" + e);
                TempData["error"] = "Failed to update";
            }
        }
        return View(movie);
    }

    public async Task<IActionResult> Details(int id)
    {
        try
        {
            Movie movie = await _movieRepository.GetByIdWithRatingsAndCommentsAsync(id);
            MovieVM movieVM = new()
            {
                Movie = movie,
                Ratings = movie.Ratings != null ? movie.Ratings.ToList() : new List<Rating>(),
                Comments = movie.Comments != null ? movie.Comments.OrderByDescending(c=>c.Id).ToList() : new List<Comment>()
            };
            bool IsRated = _rating.HasRated(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
            RatingVM ratingVm = new();

            if (IsRated)
            {
                Rating rating = await _rating.FindRatingToMovieUser(id, User.FindFirstValue(ClaimTypes.NameIdentifier));
                ratingVm = new RatingVM()
                {
                    Rating = rating,
                    MovieId = id,
                };
            }
            if (movie == null)
                return NotFound();
            MovieDetailsVM movieDetailsVM = new()
            {
                MovieVM = movieVM,
                RatingVM = ratingVm
            };
            Console.WriteLine(movieDetailsVM.MovieVM.AverageRating);
            /*Console.WriteLine(movie.Creator.UserName);*/
            return View(movieDetailsVM);
        }
        catch (Exception e)
        {
            TempData["error"] = e.Message;
            return RedirectToAction(nameof(Index));
        }
    }
    [HttpPost]
    [Route("/Movies/delete-selected-movie")]
    public async Task<IActionResult> SelectedDelete(string[] movie_ids)
    {
        try
        {

            foreach (string idString in movie_ids)
            {
                if (int.TryParse(idString, out int id))
                {
                    Movie movie = await _movieRepository.GetByIdAsync(id);
                    await _movieRepository.DeleteAsync(movie);
                }
            }
        }
        catch (Exception e)
        {
            TempData["error"] = e.Message;
            return RedirectToAction("Index", "Movies");
        }
        TempData["success"] = "Successfully Deleted !!";
        return RedirectToAction("Index", "Movies");
    }
}