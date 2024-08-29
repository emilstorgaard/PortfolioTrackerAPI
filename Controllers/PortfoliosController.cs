using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioTrackerAPI.Models.Dtos;
using PortfolioTrackerAPI.Services;

namespace PortfolioTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfoliosController : ControllerBase
    {
        private readonly PortfolioService _portfolioService;
        private readonly UserService _userService;

        public PortfoliosController(PortfolioService portfolioService, UserService userService)
        {
            _portfolioService = portfolioService;
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllPortfolios()
        {
            var userId = _userService.GetUserId();
            if (userId == null) return BadRequest("Invalid user ID in token.");

            var portfolios = await _portfolioService.GetAllPortfoliosAsync(userId.Value);
            return Ok(portfolios);
        }

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetPortfolioById(Guid id)
        {
            var userId = _userService.GetUserId();
            if (userId == null) return BadRequest("Invalid user ID in token.");
            if (id == Guid.Empty) return BadRequest("Portfolio ID is required.");

            var portfolio = await _portfolioService.GetPortfolioByIdAsync(id, userId.Value);
            if (portfolio == null) return NotFound();

            return Ok(portfolio);
        }

        [Authorize]
        [HttpGet("Overview/{id:guid}")]
        public async Task<IActionResult> GetPortfolioOverview(Guid id)
        {
            var userId = _userService.GetUserId();
            if (userId == null) return BadRequest("Invalid user ID in token.");
            if (id == Guid.Empty) return BadRequest("Portfolio ID is required.");

            var overview = await _portfolioService.GetPortfolioOverviewAsync(id, userId.Value);
            return Ok(overview);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPortfolio(PortfolioDto portfolioDto)
        {
            var userId = _userService.GetUserId();
            if (userId == null) return BadRequest("Invalid user ID in token.");
            if (portfolioDto == null) return BadRequest("Portfolio data is required.");

            var result = await _portfolioService.AddPortfolioAsync(userId.Value, portfolioDto);
            if ((bool)!result) return NotFound();

            return Ok();
        }

        [Authorize]
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdatePortfolio(Guid id, PortfolioDto portfolioDto)
        {
            var userId = _userService.GetUserId();
            if (userId == null) return BadRequest("Invalid user ID in token.");
            if (id == Guid.Empty) return BadRequest("Portfolio ID is required.");
            if (portfolioDto == null) return BadRequest("Portfolio data is required.");

            var result = await _portfolioService.UpdatePortfolioAsync(id, userId.Value, portfolioDto);
            if (result == null) return NotFound();

            return Ok();
        }

        [Authorize]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletePortfolio(Guid id)
        {
            var userId = _userService.GetUserId();
            if (userId == null) return BadRequest("Invalid user ID in token.");
            if (id == Guid.Empty) return BadRequest("Portfolio ID is required.");

            var result = await _portfolioService.DeletePortfolioAsync(id, userId.Value);
            if ((bool)!result) return NotFound();

            return Ok();
        }
    }
}
