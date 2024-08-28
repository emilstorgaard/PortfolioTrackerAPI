using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioTrackerAPI.Models.Dtos;
using PortfolioTrackerAPI.Services;

namespace PortfolioTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly TransactionService _transactionService;
        private readonly UserService _userService;

        public TransactionsController(TransactionService transactionService, UserService userService)
        {
            _transactionService = transactionService;
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllTransactions()
        {
            var userId = _userService.GetUserId();
            if (userId == null) return BadRequest("Invalid user ID in token.");

            var transactions = await _transactionService.GetAllTransactionsAsync(userId.Value);
            return Ok(transactions);
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetTransactionById(Guid id)
        {
            var userId = _userService.GetUserId();
            if (userId == null) return BadRequest("Invalid user ID in token.");
            if (id == Guid.Empty) return BadRequest("Transaction ID is required.");

            var transaction = await _transactionService.GetTransactionByIdAsync(id, userId.Value);
            if (transaction == null) return NotFound();

            return Ok(transaction);
        }

        [Authorize]
        [HttpGet("portfolio/{portfolioId:guid}")]
        public async Task<IActionResult> GetAllTransactionsByPortfolio(Guid portfolioId)
        {
            var userId = _userService.GetUserId();
            if (userId == null) return BadRequest("Invalid user ID in token.");
            if (portfolioId == Guid.Empty) return BadRequest("PortfolioId is required.");

            var transactions = await _transactionService.GetAllTransactiosByPortfolioAsync(userId.Value, portfolioId);
            return Ok(transactions);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTransaction(TransactionDto transactionDto)
        {
            var userId = _userService.GetUserId();
            if (userId == null) return BadRequest("Invalid user ID in token.");
            if (transactionDto == null) return BadRequest("Transaction data is required.");

            var result = await _transactionService.AddTransactionAsync(userId.Value, transactionDto);
            if ((bool)!result) return NotFound();

            return Ok();
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateTransaction(Guid id, [FromBody] TransactionDto transactionDto)
        {
            var userId = _userService.GetUserId();
            if (userId == null) return BadRequest("Invalid user ID in token.");
            if (id == Guid.Empty) return BadRequest("Transaction ID is required.");
            if (transactionDto == null) return BadRequest("Transaction data is required.");

            var result = await _transactionService.UpdateTransactionAsync(id, userId.Value, transactionDto);
            if (result == null) return NotFound();

            return Ok();
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            var userId = _userService.GetUserId();
            if (userId == null) return BadRequest("Invalid user ID in token.");
            if (id == Guid.Empty) return BadRequest("Transaction ID is required.");

            var result = await _transactionService.DeleteTransactionAsync(id, userId.Value);
            if ((bool)!result) return NotFound();

            return Ok();
        }
    }
}
