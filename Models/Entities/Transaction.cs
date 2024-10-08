﻿using PortfolioTrackerAPI.Models.Enums;
using System.Text.Json.Serialization;

namespace PortfolioTrackerAPI.Models.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid PortfolioId { get; set; }
        public string InvestmentName { get; set; }
        public TransactionTypeEnum Type { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalValue => Quantity * Price;
        public string Notes { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        [JsonIgnore]
        public Portfolio Portfolio { get; set; }
    }
}
