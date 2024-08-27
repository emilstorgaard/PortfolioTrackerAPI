namespace PortfolioTrackerAPI.Models.Dtos
{
    public class TransactionDto
    {
        public Guid PortfolioId { get; set; }
        public string InvestmentName { get; set; }
        public string InvestmentType { get; set; }
        public string Type { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string Notes { get; set; }
        public DateTime Date { get; set; }
    }
}
