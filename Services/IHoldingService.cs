using PortfolioTrackerAPI.Models.Dtos;

namespace PortfolioTrackerAPI.Services
{
    public interface IHoldingService
    {
        Task<List<HoldingDto>?> GetAllHoldingsByPortfolioAsync(Guid userId, Guid portfolioId)
    }
}