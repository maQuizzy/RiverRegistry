using Microsoft.EntityFrameworkCore;
using RiverRegistry.Models;

namespace RiverRegistry.Data.Contexts
{
    public class ShipsDbContext : DbContext
    {
        public DbSet<Ship> Ships { get; set; }

        public ShipsDbContext(DbContextOptions<ShipsDbContext> options) : base(options) 
        {
            Database.EnsureCreated();
        }
    }
}
