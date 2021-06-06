using System;
using System.ComponentModel.DataAnnotations;
using StonksCore.Models;

namespace StonksCore.Data.Models
{
    public class Ticker
    {
        [Key] [Required] public string TickerName { get; set; }

        [Required] public string Figi { get; set; }

        [Required] public string Isin { get; set; }

        [Required] public double MinPriceIncrement { get; set; }

        [Required] public int Lot { get; set; }

        [Required] public string Currency { get; set; }

        [Required] 
        public string Name { get; set; }

        [Required] 
        public TickerType Type { get; set; }

        [Required] 
        public int IssuerId { get; set; }
        [Required]
        public DateTime OnMarketFrom { get; set; }

        public Issuer Issuer { get; set; }
    }
}