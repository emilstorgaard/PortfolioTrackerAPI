using PortfolioTrackerAPI.Models.Dtos;

namespace PortfolioTrackerAPI.Services
{
    public interface ITotalOverviewService
    {
        Task<TotalOverviewDto> GetTotalOverviewAsync(Guid userId);
    }
}