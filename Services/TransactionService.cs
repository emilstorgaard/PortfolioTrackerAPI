using Microsoft.EntityFrameworkCore;
using PortfolioTrackerAPI.Data;
using PortfolioTrackerAPI.Models.Dtos;
using PortfolioTrackerAPI.Models.Entities;

namespace PortfolioTrackerAPI.Services
{
    public class TransactionService : ITransactionService
    {
        public readonly ApplicationDbContext _dbContext;

        public TransactionService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Transaction>> GetAllTransactiosByPortfolioAsync(Guid portfolioId)
        {
            return await _dbContext.Transactions
                .Where(t => t.PortfolioId == portfolioId)
                .ToListAsync();
        }

        public async Task<Transaction> AddTransactionAsync(AddTransactionDto addTransactionDto)
        {
            var transaction = new Transaction
            {
                PortfolioId = addTransactionDto.PortfolioId,
                InvestmentName = addTransactionDto.InvestmentName,
                InvestmentType = addTransactionDto.InvestmentType,
                Type = addTransactionDto.Type,
                Quantity = addTransactionDto.Quantity,
                Price = addTransactionDto.Price,
                Notes = addTransactionDto.Notes,
                Date = addTransactionDto.Date,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _dbContext.Transactions.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();

            return transaction;
        }
    }
}
