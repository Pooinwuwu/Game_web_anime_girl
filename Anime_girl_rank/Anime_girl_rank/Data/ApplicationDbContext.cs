using Microsoft.EntityFrameworkCore;
using Anime_girl_rank.Models;

namespace Anime_girl_rank.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Character_girl> Characters { get; set; }
    }
}
