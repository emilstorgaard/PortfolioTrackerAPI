namespace PortfolioTrackerAPI.Models.Entities
{
    public class Portfolio
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
