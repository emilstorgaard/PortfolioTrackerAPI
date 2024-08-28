using PortfolioTrackerAPI.Models.Dtos;

namespace PortfolioTrackerAPI.Services
{
    public interface IOverviewService
    {
        Task<TotalOverviewDto> GetTotalOverviewAsync(Guid userId);
    }
}