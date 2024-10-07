using Microsoft.EntityFrameworkCore;
using Pantry.Models;

namespace Pantry.Data
{
    public class PantryDBContext : DbContext
    {

        public PantryDBContext(DbContextOptions<PantryDBContext> options) : base(options) 
        {
        
        }

        public DbSet<PantryItem> PantryItems { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

    }
}
