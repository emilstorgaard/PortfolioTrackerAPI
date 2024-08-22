using Microsoft.AspNetCore.Identity;
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

        public async Task<List<Portfolio>> GetAllPortfoliosAsync()
        {
            return await _dbContext.Portfolios.ToListAsync();
        }

        public async Task<List<Portfolio>> GetAllPortfoliosByUserAsync(Guid userId)
        {
            return await _dbContext.Portfolios
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }

        public async Task<Portfolio> AddPortfolioAsync(AddPortfolioDto addPortfolioDto, Guid userId)
        {
            var portfolio = new Portfolio
            {
                Name = addPortfolioDto.Name,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _dbContext.Portfolios.AddAsync(portfolio);
            await _dbContext.SaveChangesAsync();

            return portfolio;
        }
    }
}
