using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioTrackerAPI.Services;

namespace PortfolioTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OverviewsController : ControllerBase
    {
        private readonly OverviewService _overviewService;
        private readonly UserService _userService;

        public OverviewsController(OverviewService overviewService, UserService userService)
        {
            _overviewService = overviewService;
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetTotalOverview()
        {
            var userId = _userService.GetUserId();
            if (userId == null) return BadRequest("Invalid user ID in token.");

            var Overview = await _overviewService.GetTotalOverviewAsync(userId.Value);
            return Ok(Overview);
        }
    }
}
