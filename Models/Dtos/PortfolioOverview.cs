namespace PortfolioTrackerAPI.Models.Dtos
{
    public class PortfolioOverview
    {
        public int TotalTransactions { get; set; }
        public decimal TotalInvestedAmount { get; set; }
        public decimal AverageInvestmentPerTransaction { get; set; }
        public DateTime FirstInvestmentDate { get; set; }
        public DateTime LastInvestmentDate { get; set; }
    }
}
