using PortfolioTrackerAPI.Models.Dtos;
using PortfolioTrackerAPI.Models.Entities;

namespace PortfolioTrackerAPI.Services
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllTransactiosByPortfolioAsync(Guid portfolioId);
        Task<Transaction> AddTransactionAsync(AddTransactionDto addTransactionDto);
    }
}