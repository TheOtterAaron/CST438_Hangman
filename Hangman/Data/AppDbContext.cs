using Microsoft.EntityFrameworkCore;

namespace Hangman.Data
{
    // Database Context for the Application
    // For now we just use an in-memory database
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) {}

        // Define a table for storing Game records
        public DbSet<Game> Games { get; set; }
    }
}
