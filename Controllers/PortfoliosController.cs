using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioTrackerAPI.Models.Dtos;
using PortfolioTrackerAPI.Services;
using System.Security.Claims;

namespace PortfolioTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfoliosController(PortfolioService portfolioService) : ControllerBase
    {
        private readonly PortfolioService _portfolioService = portfolioService;


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllPortfolios()
        {
            var userIdClaim = User.FindFirstValue("Uid");
            if (!Guid.TryParse(userIdClaim, out var userId)) return BadRequest("Invalid user ID in token.");

            var allPortfolios = await _portfolioService.GetAllPortfoliosByUserAsync(userId);
            return Ok(allPortfolios);
        }

        [Authorize]
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetPortfolioById(Guid id)
        {
            var portfolio = await _portfolioService.GetPortfolioByIdAsync(id);

            if (portfolio == null) return NotFound();

            return Ok(portfolio);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPortfolio(PortfolioDto portfolioDto)
        {
            var userIdClaim = User.FindFirstValue("Uid");
            if (!Guid.TryParse(userIdClaim, out var userId)) return BadRequest("Invalid user ID in token.");

            var addedPortfolio = await _portfolioService.AddPortfolioAsync(portfolioDto, userId);
            return Ok(addedPortfolio);
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePortfolio(Guid id, PortfolioDto portfolioDto)
        {
            var userIdClaim = User.FindFirstValue("Uid");
            if (!Guid.TryParse(userIdClaim, out var userId)) return BadRequest("Invalid user ID in token.");

            var updatedPortfolio = await _portfolioService.UpdatePortfolioAsync(id, userId, portfolioDto);

            if (updatedPortfolio == null) return NotFound();

            return Ok(updatedPortfolio);
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePortfolio(Guid id)
        {
            var userIdClaim = User.FindFirstValue("Uid");
            if (!Guid.TryParse(userIdClaim, out var userId)) return BadRequest("Invalid user ID in token.");

            var result = await _portfolioService.DeletePortfolioAsync(id, userId);
            if ((bool)!result) return NotFound();

            return Ok();
        }
    }
}
