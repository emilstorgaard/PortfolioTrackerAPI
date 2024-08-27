namespace PortfolioTrackerAPI.Models.Dtos
{
    public class TotalOverviewDto
    {
        public int TotalPortfolios { get; set; }

        public int TotalTransactions { get; set; }

        // Det samlede investeringsbeløb (summering af alle transaktioners TotalValue)
        public decimal TotalInvestedAmount { get; set; }

        // Eventuel gennemsnitlig investering pr. transaktion
        public decimal AverageInvestmentPerTransaction { get; set; }
        public DateTime FirstInvestmentDate { get; set; }
        public DateTime LastInvestmentDate { get; set; }

    }
}
