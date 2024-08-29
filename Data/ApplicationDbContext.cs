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
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Price)
                .HasPrecision(18, 4); // Precision 18, scale 4 (e.g., 12345678901234.5678)

            // Repeat for other decimal properties if needed
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Quantity)
                .HasPrecision(18, 4);

            // Configure EF Core to store the enum as a string
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Type)
                .HasConversion<string>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
