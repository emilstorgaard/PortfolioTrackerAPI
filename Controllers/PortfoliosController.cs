using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioTrackerAPI.Models.Dtos;
using PortfolioTrackerAPI.Services;
using System.Security.Claims;

namespace PortfolioTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfoliosController(PortfolioService portfolioService, IConfiguration configuration) : ControllerBase
    {
        private readonly PortfolioService _portfolioService = portfolioService;
        private readonly string _jwtSecret = configuration["JwtSettings:Secret"];

        [Authorize]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllPortfolios()
        {
            var allPortfolios = await _portfolioService.GetAllPortfoliosAsync();
            return Ok(allPortfolios);
        }

        [Authorize]
        [HttpGet]
        [Route("allByUser")]
        public async Task<IActionResult> GetAllPortfoliosByUser()
        {
            var userIdClaim = User.FindFirstValue("Uid");
            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                return BadRequest("Invalid user ID in token.");
            }

            var allPortfolios = await _portfolioService.GetAllPortfoliosByUserAsync(userId);
            return Ok(allPortfolios);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPortfolio(AddPortfolioDto addPortfolioDto)
        {
            var userIdClaim = User.FindFirstValue("Uid");
            if (!Guid.TryParse(userIdClaim, out var userId))
            {
                return BadRequest("Invalid user ID in token.");
            }

            var addedPortfolio = await _portfolioService.AddPortfolioAsync(addPortfolioDto, userId);
            return Ok(addedPortfolio);
        }
    }
}
