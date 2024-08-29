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

        public async Task<List<Transaction>> GetAllTransactionsAsync(Guid userId)
        {
            return await _dbContext.Transactions
                .Where(t => t.Portfolio.UserId == userId)
                .ToListAsync();
        }

        public async Task<Transaction?> GetTransactionByIdAsync(Guid id, Guid userId)
        {
            return await _dbContext.Transactions
                .FirstOrDefaultAsync(t => t.Id == id && t.Portfolio.UserId == userId);
        }

        public async Task<List<Transaction>> GetAllTransactiosByPortfolioAsync(Guid userId, Guid portfolioId)
        {
            return await _dbContext.Transactions
                .Where(t => t.PortfolioId == portfolioId && t.Portfolio.UserId == userId)
                .ToListAsync();
        }

        public async Task<bool?> AddTransactionAsync(Guid userId, TransactionDto addTransactionDto)
        {
            // Check if the portfolio exists and belongs to the user
            var portfolio = await _dbContext.Portfolios
                .FirstOrDefaultAsync(p => p.Id == addTransactionDto.PortfolioId && p.UserId == userId);

            // Portfolio doesn't exist or doesn't belong to the user
            if (portfolio == null) return null;

            if (addTransactionDto == null) return null;

            var transaction = new Transaction
            {
                PortfolioId = addTransactionDto.PortfolioId,
                InvestmentName = addTransactionDto.InvestmentName,
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

            return true;
        }

        public async Task<bool?> UpdateTransactionAsync(Guid id, Guid userId, TransactionDto transactionDto)
        {
            var transaction = await _dbContext.Transactions.FirstOrDefaultAsync(t => t.Id == id && t.Portfolio.UserId == userId);
            if (transaction == null) return null;

            transaction.InvestmentName = transactionDto.InvestmentName;
            transaction.Type = transactionDto.Type;
            transaction.Quantity = transactionDto.Quantity;
            transaction.Price = transactionDto.Price;
            transaction.Notes = transactionDto.Notes;
            transaction.Date = transactionDto.Date;

            await _dbContext.SaveChangesAsync();

            return true;
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
