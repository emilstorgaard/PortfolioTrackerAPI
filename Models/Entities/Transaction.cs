namespace PortfolioTrackerAPI.Models.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid PortfolioId { get; set; }
        public string InvestmentName { get; set; }
        public string InvestmentType { get; set; }
        public string Type { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalValue => Quantity * Price;
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
