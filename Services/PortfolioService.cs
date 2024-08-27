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

        public async Task<List<Portfolio>> GetAllPortfoliosByUserAsync(Guid userId)
        {
            return await _dbContext.Portfolios
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task<Portfolio> GetPortfolioByIdAsync(Guid id)
        {
            return await _dbContext.Portfolios.FindAsync(id);
        }

        public async Task<Portfolio> AddPortfolioAsync(PortfolioDto portfolioDto, Guid userId)
        {
            var portfolio = new Portfolio
            {
                Name = portfolioDto.Name,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _dbContext.Portfolios.AddAsync(portfolio);
            await _dbContext.SaveChangesAsync();

            return portfolio;
        }

        public async Task<Portfolio?> UpdatePortfolioAsync(Guid id, Guid userId, PortfolioDto portfolioDto)
        {
            var portfolio = await _dbContext.Portfolios.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
            if (portfolio == null) return null;

            portfolio.Name = portfolioDto.Name;
            portfolio.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            return portfolio;
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
