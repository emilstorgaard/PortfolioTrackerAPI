using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioTrackerAPI.Models.Dtos;
using PortfolioTrackerAPI.Services;
using System.Security.Claims;

namespace PortfolioTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController(TransactionService transactionService) : ControllerBase
    {
        private readonly TransactionService _transactionService = transactionService;

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            var userIdClaim = User.FindFirstValue("Uid");
            if (!Guid.TryParse(userIdClaim, out var userId)) return BadRequest("Invalid user ID in token.");

            var allTransactions = await _transactionService.GetAllTransactionsByUserAsync(userId);
            return Ok(allTransactions);
        }

        [Authorize]
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetTransactionById(Guid id)
        {
            if (id == Guid.Empty) return BadRequest("Transaction ID is required.");

            var userIdClaim = User.FindFirstValue("Uid");
            if (!Guid.TryParse(userIdClaim, out var userId)) return BadRequest("Invalid user ID in token.");


            var transaction = await _transactionService.GetTransactionByIdAsync(id, userId);
            return Ok(transaction);
        }

        [Authorize]
        [HttpGet]
        [Route("portfolio/{portfolioId:guid}")]
        public async Task<IActionResult> GetAllTransactionsByPortfolio(Guid portfolioId)
        {
            if (portfolioId == Guid.Empty) return BadRequest("PortfolioId is required.");

            var alltransactions = await _transactionService.GetAllTransactiosByPortfolioAsync(portfolioId);
            return Ok(alltransactions);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTransaction(TransactionDto addTransactionDto)
        {
            var addedTransaction = await _transactionService.AddTransactionAsync(addTransactionDto);
            return Ok(addedTransaction);
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTransaction(Guid id, TransactionDto transactionDto)
        {
            var userIdClaim = User.FindFirstValue("Uid");
            if (!Guid.TryParse(userIdClaim, out var userId)) return BadRequest("Invalid user ID in token.");

            var updatedTransaction = await _transactionService.UpdateTransactionAsync(id, userId, transactionDto);

            if (updatedTransaction == null) return NotFound();

            return Ok(updatedTransaction);
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            var userIdClaim = User.FindFirstValue("Uid");
            if (!Guid.TryParse(userIdClaim, out var userId)) return BadRequest("Invalid user ID in token.");

            var result = await _transactionService.DeleteTransactionAsync(id, userId);
            if ((bool)!result) return NotFound();

            return Ok();
        }
    }
}
