using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioTrackerAPI.Services;

namespace PortfolioTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TotalOverviewController : ControllerBase
    {
        private readonly TotalOverviewService _totalOverviewService;
        private readonly UserService _userService;

        public TotalOverviewController(TotalOverviewService totalOverviewService, UserService userService)
        {
            _totalOverviewService = totalOverviewService;
            _userService = userService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetTotalOverview()
        {
            var userId = _userService.GetUserId();
            if (userId == null) return BadRequest("Invalid user ID in token.");

            var totalOverview = await _totalOverviewService.GetTotalOverviewAsync(userId.Value);
            return Ok(totalOverview);
        }
    }
}
