using Microsoft.EntityFrameworkCore;
using PortfolioTrackerAPI.Data;
using PortfolioTrackerAPI.Models.Dtos;

namespace PortfolioTrackerAPI.Services
{
    public class OverviewService : IOverviewService
    {
        public readonly ApplicationDbContext _dbContext;

        public OverviewService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TotalOverviewDto> GetTotalOverviewAsync()
        {
            var totalPortfolios = await _dbContext.Portfolios.CountAsync();

            var transactions = _dbContext.Transactions.AsQueryable();

            var totalTransactions = await transactions.CountAsync();
            var totalInvestedAmount = await transactions.SumAsync(t => t.Quantity * t.Price); // Ændret her
            var firstInvestmentDate = await transactions.MinAsync(t => t.Date);
            var lastInvestmentDate = await transactions.MaxAsync(t => t.Date);

            var averageInvestmentPerTransaction = totalTransactions > 0
                ? totalInvestedAmount / totalTransactions
                : 0;

            var totalOverview = new TotalOverviewDto
            {
                TotalPortfolios = totalPortfolios,
                TotalTransactions = totalTransactions,
                TotalInvestedAmount = totalInvestedAmount,
                AverageInvestmentPerTransaction = averageInvestmentPerTransaction,
                FirstInvestmentDate = firstInvestmentDate,
                LastInvestmentDate = lastInvestmentDate
            };

            return totalOverview;
        }
    }
}
