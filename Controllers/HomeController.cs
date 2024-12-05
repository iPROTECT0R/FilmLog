using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FilmLog.Models;

namespace FilmLog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Injecting the logger service to log information, warnings, or errors
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // This action handles the homepage (the default landing page) and redirects to the film list
        public IActionResult Index()
        {
            // Instead of showing a homepage, we automatically redirect the user to the FilmController's Index action
            return RedirectToAction("Index", "Film");
        }

        // This action returns the Privacy page
        public IActionResult Privacy()
        {
            return View(); // Show the privacy view
        }

        // This action handles errors. It caches the error view and shows information about the request.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // If there's an error, show the error page with a request ID (for debugging purposes)
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
