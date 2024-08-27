using PortfolioTrackerAPI.Models.Dtos;
using PortfolioTrackerAPI.Models.Entities;

namespace PortfolioTrackerAPI.Services
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllTransactionsByUserAsync(Guid userId);
        Task<Transaction> GetTransactionByIdAsync(Guid id, Guid userId);
        Task<List<Transaction>> GetAllTransactiosByPortfolioAsync(Guid portfolioId);
        Task<Transaction> AddTransactionAsync(TransactionDto addTransactionDto);
        Task<Transaction?> UpdateTransactionAsync(Guid id, Guid userId, TransactionDto transactionDto);
    }
}