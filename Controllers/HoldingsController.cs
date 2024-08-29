using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioTrackerAPI.Services;

namespace PortfolioTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoldingsController : ControllerBase
    {
        private readonly HoldingService _holdingService;
        private readonly UserService _userService;

        public HoldingsController(HoldingService holdingService, UserService userService)
        {
            _holdingService = holdingService;
            _userService = userService;
        }

        [Authorize]
        [HttpGet("Portfolio/{portfolioId:guid}")]
        public async Task<IActionResult> GetHoldings(Guid portfolioId)
        {
            var userId = _userService.GetUserId();
            if (userId == null) return BadRequest("Invalid user ID in token.");

            var holdings = await _holdingService.GetAllHoldingsByPortfolioAsync(userId.Value, portfolioId);
            return Ok(holdings);
        }
    }
}
