using Microsoft.EntityFrameworkCore;
using PortfolioTrackerAPI.Data;
using PortfolioTrackerAPI.Models.Dtos;

namespace PortfolioTrackerAPI.Services
{
    public class TotalOverviewService : ITotalOverviewService
    {
        public readonly ApplicationDbContext _dbContext;

        public TotalOverviewService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TotalOverviewDto> GetTotalOverviewAsync(Guid userId)
        {
            var totalOverview = new TotalOverviewDto { };

            // Filter portfolios by userId
            var userPortfolios = _dbContext.Portfolios.Where(p => p.UserId == userId);

            // Count the total number of portfolios for the user
            var totalPortfolios = await userPortfolios.CountAsync();
            totalOverview.TotalPortfolios = totalPortfolios;

            // Filter transactions by portfolios belonging to the user
            var userTransactions = _dbContext.Transactions.Where(t => userPortfolios.Any(p => p.Id == t.PortfolioId));

            // Calculate total number of transactions for the user
            var totalTransactions = await userTransactions.CountAsync();
            totalOverview.TotalTransactions = totalTransactions;

            // Calculate total invested amount for the user
            var totalInvestedAmount = await userTransactions.SumAsync(t => t.Quantity * t.Price);
            totalOverview.TotalInvestedAmount = totalInvestedAmount;

            // Only perform these calculations if there are transactions
            if (totalTransactions > 0)
            {
                // Get the first and last investment dates for the user
                var firstInvestmentDate = await userTransactions.MinAsync(t => t.Date);
                totalOverview.FirstInvestmentDate = firstInvestmentDate;

                var lastInvestmentDate = await userTransactions.MaxAsync(t => t.Date);
                totalOverview.LastInvestmentDate = lastInvestmentDate;
            }

            var averageInvestmentPerTransaction = totalTransactions > 0
                ? totalInvestedAmount / totalTransactions
                : 0;

            totalOverview.AverageInvestmentPerTransaction = averageInvestmentPerTransaction;
            
            return totalOverview;
        }
    }
}
