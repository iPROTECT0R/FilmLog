using FilmLog.Data;
using FilmLog.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

public static class FilmSeeder
{
    // This method will seed (add) a set of films to the database if the database is empty
    public static async Task SeedFilms(ApplicationDbContext context)
    {
        // Check if any films are already in the database; if so, do nothing
        if (context.Films.Any()) return;

        // Define a list of films to add to the database
        var films = new[]
        {
            // Each film has a title, description, director, and release date
            new Film { Title = "The Shawshank Redemption", Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.", Director = "Frank Darabont", ReleaseDate = new DateTime(1994, 9, 23) },
            new Film { Title = "The Godfather", Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.", Director = "Francis Ford Coppola", ReleaseDate = new DateTime(1972, 3, 24) },
            new Film { Title = "The Dark Knight", Description = "When the menace known as The Joker emerges from his mysterious past, he wreaks havoc and chaos on the people of Gotham.", Director = "Christopher Nolan", ReleaseDate = new DateTime(2008, 7, 18) },
            new Film { Title = "Pulp Fiction", Description = "The lives of two mob hitmen, a boxer, a gangster's wife, and a pair of diner bandits intertwine in four tales of violence and redemption.", Director = "Quentin Tarantino", ReleaseDate = new DateTime(1994, 10, 14) },
            new Film { Title = "Forrest Gump", Description = "The presidencies of Kennedy and Johnson, the Vietnam War, the Watergate scandal and other historical events unfold from the perspective of an Alabama man with an extraordinary degree of success.", Director = "Robert Zemeckis", ReleaseDate = new DateTime(1994, 7, 6) },
            new Film { Title = "The Matrix", Description = "A computer hacker learns from mysterious rebels about the true nature of his reality and his role in the war against its controllers.", Director = "The Wachowskis", ReleaseDate = new DateTime(1999, 3, 31) },
            new Film { Title = "Inception", Description = "A thief who steals corporate secrets through the use of dream-sharing technology is given the inverse task of planting an idea into the mind of a CEO.", Director = "Christopher Nolan", ReleaseDate = new DateTime(2010, 7, 16) },
            new Film { Title = "The Lion King", Description = "Lion prince Simba and his father are targeted by his bitter uncle, who wants to ascend the throne himself.", Director = "Roger Allers, Rob Minkoff", ReleaseDate = new DateTime(1994, 6, 15) },
            new Film { Title = "Gladiator", Description = "A former Roman General sets out to exact vengeance against the corrupt emperor who murdered his family and sent him into slavery.", Director = "Ridley Scott", ReleaseDate = new DateTime(2000, 5, 5) },
            new Film { Title = "Titanic", Description = "A seventeen-year-old aristocrat falls in love with a kind but poor artist aboard the luxurious, ill-fated R.M.S. Titanic.", Director = "James Cameron", ReleaseDate = new DateTime(1997, 12, 19) }
        };

        // Add the list of films to the context (i.e., prepare them to be saved to the database)
        context.Films.AddRange(films);

        // Save the films to the database asynchronously (actually add them to the database)
        await context.SaveChangesAsync();
    }
}
