using Apartments_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Apartment_API.Database
{
    
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        public DbSet<Apartment> Apartments { get; set; }
    }
}
