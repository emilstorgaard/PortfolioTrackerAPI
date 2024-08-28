using PortfolioTrackerAPI.Models.Dtos;
using PortfolioTrackerAPI.Models.Entities;

namespace PortfolioTrackerAPI.Services
{
    public interface IPortfolioService
    {
        Task<List<Portfolio>> GetAllPortfoliosAsync(Guid userId);
        Task<Portfolio?> GetPortfolioByIdAsync(Guid id, Guid userId);
        Task<bool?> AddPortfolioAsync(Guid userId, PortfolioDto portfolioDto);
        Task<bool?> UpdatePortfolioAsync(Guid id, Guid userId, PortfolioDto portfolioDto);
        Task<bool?> DeletePortfolioAsync(Guid id, Guid userId);
    }
}