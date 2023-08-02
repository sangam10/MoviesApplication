using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesApplication.Data.Interfaces;
using MoviesApplication.Data.Repository;
using MoviesApplication.Models.ViewModels;
using MoviesApplication.Models;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MoviesApplication.Controllers
{
    public class RatingController : Controller
    {
        private readonly IRating _ratingRepository;
        public RatingController(IRating ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(RatingVM ratingVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //ApplicationUser currentUser = await _userManager.GetUserAsync(User);
                    bool hasRated = _ratingRepository.HasRated(ratingVM.MovieId, User.FindFirstValue(ClaimTypes.NameIdentifier));
                    Rating newRating = new()
                    {
                        Stars = ratingVM.Rating.Stars,
                        MovieId = ratingVM.MovieId,
                        ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                    };

                    if (!hasRated)
                        await _ratingRepository.AddAsync(newRating);
                    else
                    {
                        Rating rating = await _ratingRepository.FindRatingToMovieUser(ratingVM.MovieId, User.FindFirstValue(ClaimTypes.NameIdentifier));
                        await this.Update(rating.Id, newRating);
                    }

                    return RedirectToAction("Details", "Movies", new { id = ratingVM.MovieId });
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.ToString();
                    return RedirectToAction("Details", "Movies", new { id = ratingVM.MovieId });
                }
            }

            TempData["error"] = "Failed to Rate!!";
            return RedirectToAction("Details", "Movies", new { id = ratingVM.MovieId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task Update(int id, Rating rating)
        {
            Rating r = await _ratingRepository.GetByIdAsync(id);
            if(r != null)
            {
                r.Stars = rating.Stars;
            }
            await _ratingRepository.UpdateAsync(r);
        }
    }
}
