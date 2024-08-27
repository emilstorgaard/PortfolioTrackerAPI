using PortfolioTrackerAPI.Models.Dtos;
using PortfolioTrackerAPI.Models.Entities;

namespace PortfolioTrackerAPI.Services
{
    public interface IPortfolioService
    {
        Task<List<Portfolio>> GetAllPortfoliosByUserAsync(Guid userId);
        Task<Portfolio> GetPortfolioByIdAsync(Guid id);
        Task<Portfolio> AddPortfolioAsync(PortfolioDto addPortfolioDto, Guid userId);
        Task<Portfolio?> UpdatePortfolioAsync(Guid id, Guid userId, PortfolioDto portfolioDto);
        Task<bool?> DeletePortfolioAsync(Guid id, Guid userId);
    }
}