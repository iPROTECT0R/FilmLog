using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FilmLog.Models;

namespace FilmLog.Data
{
    // ApplicationDbContext is the class that connects to the database and manages the data
    // It extends IdentityDbContext to handle user authentication and authorization
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        // Constructor: This is where we configure the context with the options passed in (like the database connection settings)
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSet<Film> represents the "Films" table in the database.
        // It allows us to interact with the data in the "Films" table (CRUD operations like Create, Read, Update, Delete)
        public DbSet<Film> Films { get; set; }
    }
}
