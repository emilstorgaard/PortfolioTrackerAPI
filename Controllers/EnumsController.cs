using Microsoft.AspNetCore.Mvc;
using PortfolioTrackerAPI.Models.Dtos;
using PortfolioTrackerAPI.Models.Enums;

namespace PortfolioTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnumsController : ControllerBase
    {
        [HttpGet("transaction-types")]
        public ActionResult<IEnumerable<EnumValueDto>> GetTransactionTypes()
        {
            var values = Enum.GetValues(typeof(TransactionTypeEnum))
            .Cast<TransactionTypeEnum>()
            .Select(e => new EnumValueDto
            {
                Value = (int)e,
                Name = e.ToString()
            })
            .ToList();

            return Ok(values);
        }
    }
}
