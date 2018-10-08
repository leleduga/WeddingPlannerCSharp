using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;

namespace WeddingPlanner.Data
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options)
        : base (options ){ }
        public DbSet<User> Users { get; set; }
        public DbSet<Wedding> Weddings { get; set; } 
        public DbSet<Guest> Guests { get; set; }
    }
}