using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoviesApplication.Data.Repository;
using MoviesApplication.Models.ViewModels;
using MoviesApplication.Models;
using System.Security.Claims;
using MoviesApplication.Data.Interfaces;

namespace MoviesApplication.Controllers
{
    public class CommentController : Controller
    {
        private readonly IComment _commentRepository;
        public CommentController(IComment commentRepository)
        {
            _commentRepository = commentRepository;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(CommentVM commentVM)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Comment newComment = new()
                    {
                        Text = commentVM.Comment.Text,
                        MovieId = commentVM.MovieId,
                        ApplicationUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                    };


                    await _commentRepository.AddAsync(newComment);
                    return RedirectToAction("Details", "Movies", new { id = commentVM.MovieId });
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.ToString();
                    return RedirectToAction("Details", "Movies", new { id = commentVM.MovieId });
                }
            }

            TempData["error"] = "Failed to Create!!";
            return RedirectToAction("Details", "Movies", new { id = commentVM.MovieId });
        }
    }
}
