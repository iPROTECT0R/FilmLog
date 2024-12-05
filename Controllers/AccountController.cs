using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FilmLog.Models;
using System.Threading.Tasks;

namespace FilmLog.Controllers
{
    public class AccountController : Controller
    {
        // We're using UserManager to handle user operations and SignInManager for login
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        // Constructor: We're setting up our UserManager and SignInManager so we can use them in this controller
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // This shows the registration form to the user
        [HttpGet]
        public IActionResult Register()
        {
            return View(); // Just return the registration view
        }

        // This handles the form submission when a user tries to register
        [HttpPost]
        [ValidateAntiForgeryToken] // Helps protect against cross-site request forgery
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) // Only proceed if the form is valid
            {
                // Create a new user with the email and password from the registration form
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password); // Try to create the user

                if (result.Succeeded) // If user creation was successful
                {
                    // Log the user in right away (without keeping them logged in for the long term)
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    // Show a success message and send them to the login page
                    TempData["SuccessMessage"] = "Account created successfully! Please log in.";
                    return RedirectToAction("Login");
                }

                // If there were any issues with creating the user, show the error messages
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If the model isn't valid or something went wrong, show the form again with errors
            return View(model);
        }

        // This shows the login form
        [HttpGet]
        public IActionResult Login()
        {
            return View(); // Return the login view
        }

        // This handles the login form submission when the user tries to log in
        [HttpPost]
        [ValidateAntiForgeryToken] // Helps protect against cross-site request forgery
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid) // Only proceed if the form is valid
            {
                // Try to sign the user in using the email and password they provided
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded) // If login is successful
                {
                    // Show a success message and redirect them to the homepage
                    TempData["SuccessMessage"] = "You have logged in successfully!";
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // If login fails, show an error message on the login form
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            // If the model isn't valid or there was an error, return the login form with the error message
            return View(model);
        }

        // This handles the logout action
        [HttpPost]
        [ValidateAntiForgeryToken] // Helps protect against cross-site request forgery
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync(); // Log the user out

            // After logging out, redirect them back to the login page
            return Redirect("http://localhost:5138/Account/Login");
        }
    }
}
