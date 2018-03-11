using Microsoft.EntityFrameworkCore;

namespace Hangman.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) {}

        public DbSet<Game> Games { get; set; }
    }
}
