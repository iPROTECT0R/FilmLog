using FilmLog.Data;
using FilmLog.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FilmLog.Controllers
{
    [Authorize]  // This attribute makes sure the user is logged in before they can access any action in this controller
    public class FilmController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Injecting the application's database context to interact with the Films data
        public FilmController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Films - This is the page that shows the list of all films, and allows for searching
        public async Task<IActionResult> Index(string searchQuery)
        {
            // If there's no search term, just show all films, otherwise filter films based on the search query
            var films = string.IsNullOrEmpty(searchQuery)
                ? await _context.Films.ToListAsync()  // No search: Get all films
                : await _context.Films
                    .Where(f => f.Title.ToLower().Contains(searchQuery.ToLower()) || 
                                f.Description.ToLower().Contains(searchQuery.ToLower()) || 
                                f.Director.ToLower().Contains(searchQuery.ToLower()))
                    .ToListAsync(); // Search: Filter films based on title, description, or director

            return View(films); // Return the filtered or all films to the view
        }

        // This handles suggestions for film titles as the user types in the search bar
        [HttpGet]
        public async Task<IActionResult> SearchSuggestions(string term)
        {
            if (string.IsNullOrEmpty(term)) // If there's no search term, return no suggestions
            {
                return Json(new string[] { });
            }

            // Film titles that match the search term (case-insensitive)
            var suggestions = await _context.Films
                .Where(f => f.Title.ToLower().Contains(term.ToLower()))
                .Select(f => f.Title)  // Only return the title (not the whole film)
                .Distinct()  // Remove duplicates
                .Take(10)  
                .ToListAsync();

            return Json(suggestions); // Return the suggestions as a JSON response
        }

        // GET: Films/Create - This shows the form for creating a new film
        public IActionResult Create()
        {
            return View(); // Return the create film form view
        }

        // POST: Films/Create - This handles the form submission when creating a new film
        [HttpPost]
        [ValidateAntiForgeryToken]  // Helps prevent cross-site request forgery
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Director,ReleaseDate")] Film film)
        {
            if (ModelState.IsValid) // Only proceed if the form is valid (no errors)
            {
                _context.Add(film);  // Add the new film to the database context
                await _context.SaveChangesAsync();  // Save the changes (i.e., create the film in the DB)
                return RedirectToAction(nameof(Index));  // Redirect back to the list of films
            }

            return View(film);  // If the form is invalid, return the create view with the form data and errors
        }

        // GET: Films/Edit/5 - This shows the form for editing an existing film
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)  // If no ID is provided, something went wrong
            {
                return NotFound();  // Return a 404 error (film not found)
            }

            var film = await _context.Films.FindAsync(id);  // Try to find the film in the database by its ID
            if (film == null)  // If the film doesn't exist, return a 404 error
            {
                return NotFound();
            }

            return View(film);  // Return the edit form with the film details
        }

        // POST: Films/Edit/5 - This handles the form submission when editing a film
        [HttpPost]
        [ValidateAntiForgeryToken]  // Helps prevent cross-site request forgery
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Director,ReleaseDate")] Film film)
        {
            if (id != film.Id)  // If the ID in the URL doesn't match the film ID, something is wrong
            {
                return NotFound();  // Return a 404 error (film not found)
            }

            if (ModelState.IsValid) // Only proceed if the form is valid
            {
                try
                {
                    _context.Update(film);  // Update the film in the database
                    await _context.SaveChangesAsync();  // Save the changes
                }
                catch (DbUpdateConcurrencyException)  // If another user updated the film at the same time
                {
                    if (!_context.Films.Any(e => e.Id == film.Id))  // Check if the film still exists
                    {
                        return NotFound();  // If the film no longer exists, return a 404 error
                    }
                    else
                    {
                        throw;  // If the error is different, rethrow the exception
                    }
                }
                return RedirectToAction(nameof(Index));  // If successful, redirect back to the films list
            }

            return View(film);  // If the form is invalid, return to the edit form with errors
        }

        // GET: Films/Delete/5 - This shows a confirmation page before deleting a film
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)  // If no ID is provided, return a 404 error
            {
                return NotFound();
            }

            var film = await _context.Films
                .FirstOrDefaultAsync(m => m.Id == id);  // Try to find the film by its ID
            if (film == null)  // If the film doesn't exist, return a 404 error
            {
                return NotFound();
            }

            return View(film);  // Show the delete confirmation page with the film details
        }

        // POST: Films/Delete/5 - This handles the actual deletion of a film from the database
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]  // Helps prevent cross-site request forgery
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var film = await _context.Films.FindAsync(id);  // Find the film by its ID
            _context.Films.Remove(film);  // Remove the film from the database
            await _context.SaveChangesAsync();  // Save the changes (i.e., delete the film)
            return RedirectToAction(nameof(Index));  // Redirect back to the list of films
        }
    }
}
