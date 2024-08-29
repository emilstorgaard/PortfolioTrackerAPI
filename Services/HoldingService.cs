using Microsoft.EntityFrameworkCore;
using PortfolioTrackerAPI.Data;
using PortfolioTrackerAPI.Models.Dtos;
using PortfolioTrackerAPI.Models.Enums;

namespace PortfolioTrackerAPI.Services
{
    public class HoldingService : IHoldingService
    {
        public readonly ApplicationDbContext _dbContext;

        public HoldingService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<HoldingDto>?> GetAllHoldingsByPortfolioAsync(Guid userId, Guid portfolioId)
        {
            // Check if the portfolio exists and belongs to the user
            var portfolio = await _dbContext.Portfolios
                .FirstOrDefaultAsync(p => p.Id == portfolioId && p.UserId == userId);

            // Portfolio doesn't exist or doesn't belong to the user
            if (portfolio == null) return null;

            // Filter transactions by portfolios belonging to the user
            var portfolioTransactions = _dbContext.Transactions.Where(t => t.PortfolioId == portfolioId && t.Portfolio.UserId == userId);

            // Group by InvestmentName, sum the Quantity considering the transaction type, and project to HoldingDto
            var holdings = await portfolioTransactions
                .GroupBy(t => t.InvestmentName)
                .Select(g => new HoldingDto
                {
                    Name = g.Key, // g.Key is the InvestmentName in each group
                    Quantity = g.Sum(t => t.Type == TransactionTypeEnum.Buy ? t.Quantity : -t.Quantity)
                })
                .ToListAsync();

            return holdings;
        }
    }
}
