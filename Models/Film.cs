namespace FilmLog.Models
{
    // The Film class represents a movie or film in the system.
    // It includes basic information about the film like its title, description, director, and release date.
    public class Film
    {
        // Unique identifier for the film (e.g., a primary key in the database).
        public int Id { get; set; }

        // The title of the film (e.g., "The Shawshank Redemption").
        public string Title { get; set; }

        // A brief description or summary of the film's plot.
        public string Description { get; set; }

        // The director(s) of the film (e.g., "Frank Darabont").
        public string Director { get; set; }

        // The release date of the film (e.g., "1994-09-23").
        public DateTime ReleaseDate { get; set; }
    }
}
