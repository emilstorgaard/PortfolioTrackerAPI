using PortfolioTrackerAPI.Models.Dtos;
using PortfolioTrackerAPI.Models.Entities;

namespace PortfolioTrackerAPI.Services
{
    public interface IPortfolioService
    {
        Task<List<Portfolio>> GetAllPortfoliosAsync();
        Task<List<Portfolio>> GetAllPortfoliosByUserAsync(Guid userId);
        Task<Portfolio> AddPortfolioAsync(AddPortfolioDto addPortfolioDto, Guid userId);
    }
}