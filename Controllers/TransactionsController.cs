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
        [Route("allByPortfolio/{portfolioId:guid}")]
        public async Task<IActionResult> GetAllTransactionsByPortfolio(Guid portfolioId)
        {
            if (portfolioId == Guid.Empty)
            {
                return BadRequest("PortfolioId is required.");
            }

            var allPortfolios = await _transactionService.GetAllTransactiosByPortfolioAsync(portfolioId);
            return Ok(allPortfolios);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTransaction(AddTransactionDto addTransactionDto)
        {
            var addedTransaction = await _transactionService.AddTransactionAsync(addTransactionDto);
            return Ok(addedTransaction);
        }
    }
}
