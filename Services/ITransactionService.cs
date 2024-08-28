using PortfolioTrackerAPI.Models.Dtos;
using PortfolioTrackerAPI.Models.Entities;

namespace PortfolioTrackerAPI.Services
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetAllTransactionsAsync(Guid userId);
        Task<Transaction?> GetTransactionByIdAsync(Guid id, Guid userId);
        Task<List<Transaction>> GetAllTransactiosByPortfolioAsync(Guid userId, Guid portfolioId);
        Task<bool?> AddTransactionAsync(Guid userId, TransactionDto addTransactionDto);
        Task<bool?> UpdateTransactionAsync(Guid id, Guid userId, TransactionDto transactionDto);
        Task<bool?> DeleteTransactionAsync(Guid id, Guid userId);
    }
}