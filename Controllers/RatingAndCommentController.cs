
using MoviesApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using MoviesApplication.Data.Interfaces;
using MoviesApplication.Models.ViewModels;
using MoviesApplication.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace MoviesApplication.Controllers;

public class RatingAndCommentController : Controller
{
    private readonly IRating _ratingRepository;
    private readonly IComment _commentRepository;
    private readonly UserManager<ApplicationUser> _userManager;

    public RatingAndCommentController
        (
        IRating ratingRepository, 
        IComment commentRepository,
        UserManager<ApplicationUser> userManager
        )
    {
        _ratingRepository = ratingRepository;
        _commentRepository = commentRepository;
        _userManager = userManager;
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
    public async Task<IActionResult> Create(RatingAndCommentVM ratingAndCommentVM)
    {
        if (ModelState.IsValid)
        {
            try
            {
                //ApplicationUser currentUser = await _userManager.GetUserAsync(User);
                bool hasRated = _ratingRepository.HasRated(ratingAndCommentVM.movieId,User.FindFirstValue(ClaimTypes.NameIdentifier));
                if(!hasRated)
                {
                    Rating newRating = new ()
                    {
                        Stars = ratingAndCommentVM.Rating.Stars,
                        //Comment = rating.Comment,
                        MovieId = ratingAndCommentVM.movieId,
                        ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                    };
                    await _ratingRepository.AddAsync(newRating);
                }
                
                Comment newComment = new ()
                {
                    Text = ratingAndCommentVM.Comment.Text,
                    MovieId = ratingAndCommentVM.movieId,
                    ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };

                
                await _commentRepository.AddAsync(newComment);
                return RedirectToAction("Details", "Movies", new { id = ratingAndCommentVM.movieId });
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.ToString();
                return RedirectToAction("Details","Movies", new { id = ratingAndCommentVM.movieId });
            } 
        }

        TempData["error"] = "Failed to Create!!";
        return RedirectToAction("Details", "Movies",new {id = ratingAndCommentVM.movieId });
    }

/*
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize]
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
    [Authorize]
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
    }*/
}