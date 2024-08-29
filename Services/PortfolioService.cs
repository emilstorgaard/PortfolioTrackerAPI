using Microsoft.EntityFrameworkCore;
using PortfolioTrackerAPI.Data;
using PortfolioTrackerAPI.Models.Dtos;
using PortfolioTrackerAPI.Models.Entities;

namespace PortfolioTrackerAPI.Services
{
    public class PortfolioService : IPortfolioService
    {
        public readonly ApplicationDbContext _dbContext;

        public PortfolioService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Portfolio>> GetAllPortfoliosAsync(Guid userId)
        {
            return await _dbContext.Portfolios
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task<Portfolio?> GetPortfolioByIdAsync(Guid id, Guid userId)
        {
            return await _dbContext.Portfolios
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
        }

        public async Task<PortfolioOverview?> GetPortfolioOverviewAsync(Guid id, Guid userId)
        {
            // Check if the portfolio exists and belongs to the user
            var portfolio = await _dbContext.Portfolios
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

            // Portfolio doesn't exist or doesn't belong to the user
            if (portfolio == null) return null;

            var portfolioOverview = new PortfolioOverview { };

            // Filter transactions by portfolios belonging to the user
            var portfolioTransactions = _dbContext.Transactions.Where(t => t.PortfolioId == id && t.Portfolio.UserId == userId);


            // Calculate total number of transactions for the portfolio
            var totalTransactions = await portfolioTransactions.CountAsync();
            portfolioOverview.TotalTransactions = totalTransactions;

            // Calculate total invested amount for the user
            var totalInvestedAmount = await portfolioTransactions.SumAsync(t => t.Quantity * t.Price);
            portfolioOverview.TotalInvestedAmount = totalInvestedAmount;

            // Only perform these calculations if there are transactions
            if (totalTransactions > 0)
            {
                // Get the first and last investment dates for the user
                var firstInvestmentDate = await portfolioTransactions.MinAsync(t => t.Date);
                portfolioOverview.FirstInvestmentDate = firstInvestmentDate;

                var lastInvestmentDate = await portfolioTransactions.MaxAsync(t => t.Date);
                portfolioOverview.LastInvestmentDate = lastInvestmentDate;
            }

            var averageInvestmentPerTransaction = totalTransactions > 0
                ? totalInvestedAmount / totalTransactions
                : 0;

            portfolioOverview.AverageInvestmentPerTransaction = averageInvestmentPerTransaction;

            return portfolioOverview;
        }

        public async Task<bool?> AddPortfolioAsync(Guid userId, PortfolioDto portfolioDto)
        {
            if (portfolioDto == null) return null;

            var portfolio = new Portfolio
            {
                Name = portfolioDto.Name,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _dbContext.Portfolios.AddAsync(portfolio);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool?> UpdatePortfolioAsync(Guid id, Guid userId, PortfolioDto portfolioDto)
        {
            var portfolio = await _dbContext.Portfolios.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
            if (portfolio == null) return null;

            portfolio.Name = portfolioDto.Name;
            portfolio.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool?> DeletePortfolioAsync(Guid id, Guid userId)
        {
            var portfolio = await _dbContext.Portfolios.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
            if (portfolio == null) return null;

            _dbContext.Portfolios.Remove(portfolio);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
