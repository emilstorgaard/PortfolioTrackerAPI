using Microsoft.EntityFrameworkCore;
using PortfolioTrackerAPI.Models.Entities;

namespace PortfolioTrackerAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                   
        }

        public DbSet<Portfolio> Portfolios { get; set; }
    }
}
