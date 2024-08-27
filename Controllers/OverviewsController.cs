using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioTrackerAPI.Services;

namespace PortfolioTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OverviewsController(OverviewService overviewService) : ControllerBase
    {
        private readonly OverviewService _overviewService = overviewService;

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllPortfolios()
        {
            var Overview = await _overviewService.GetTotalOverviewAsync();
            return Ok(Overview);
        }
    }
}
