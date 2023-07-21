using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MoviesApplication.Data.Interfaces;
using MoviesApplication.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MoviesApplication.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieRepository _repo;
        //private readonly UserManager<ApplicationUser> _userManager;
        public MoviesController(IMovieRepository repo)
        {
            _repo = repo;
        }
        // GET: MoviesController
        public async Task<ActionResult> Index()
        {
            IEnumerable<Movie> moviesList = await _repo.GetAllAsync();
            return View(moviesList);
        }

        // GET: MoviesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MoviesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MoviesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Movie movie)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(userId == null)
            {
                TempData["error"] = "Login First !!";
                return View();
            }
            movie.ApplicationUserId = userId;
            if(await _repo.AddAsync(movie))
                TempData["success"] = "Movie created Successfully";
            return View();
        }

        // GET: MoviesController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Movie movie = await _repo.GetByIdAsync(id);
            if (movie == null)
            {
                TempData["error"] = "Data Not Found!!";
                return View();
            }
            return View(movie);
        }

        // POST: MoviesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id,[Bind("Id","Name","Poster_Image","Release_Date")] Movie movie)
        {
            if(id == null)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                if(_repo.UpdateAsync(movie))
                {
                    TempData["success"] = "Update Successful !!";
                    return View();
                }
            }
            TempData["error"] = "Failed to Update !!";
            return View("Edit");
        }

        // POST: MoviesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            bool isDeleted =await _repo.DeleteAsync(id);
            if (isDeleted)
            {
                TempData["success"] = "Movie Successfully deleted!!";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "Failed to delete movie!!";
            return RedirectToAction(nameof(Index));
        }
    }
}
