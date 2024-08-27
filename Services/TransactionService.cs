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

        public async Task<List<Transaction>> GetAllTransactionsByUserAsync(Guid userId)
        {
            return await _dbContext.Transactions
                .Where(t => t.Portfolio.UserId == userId)
                .ToListAsync();
        }

        public async Task<Transaction> GetTransactionByIdAsync(Guid id, Guid userId)
        {
            return await _dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == id && t.Portfolio.UserId == userId);
        }

        public async Task<List<Transaction>> GetAllTransactiosByPortfolioAsync(Guid portfolioId)
        {
            return await _dbContext.Transactions
                .Where(t => t.PortfolioId == portfolioId)
                .ToListAsync();
        }

        public async Task<Transaction> AddTransactionAsync(TransactionDto addTransactionDto)
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

        public async Task<Transaction?> UpdateTransactionAsync(Guid id, Guid userId, TransactionDto transactionDto)
        {
            var transaction = await _dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == id && t.Portfolio.UserId == userId);
            if (transaction == null) return null;

            transaction.InvestmentName = transactionDto.InvestmentName;
            transaction.InvestmentType = transactionDto.InvestmentType;
            transaction.Type = transactionDto.Type;
            transaction.Quantity = transactionDto.Quantity;
            transaction.Price = transactionDto.Price;
            transaction.Notes = transactionDto.Notes;
            transaction.Date = transactionDto.Date;

            await _dbContext.SaveChangesAsync();

            return transaction;
        }

        public async Task<bool?> DeleteTransactionAsync(Guid id, Guid userId)
        {
            var transaction = await _dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == id && t.Portfolio.UserId == userId);
            if (transaction == null) return null;

            _dbContext.Transactions.Remove(transaction);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
