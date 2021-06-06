using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StonksCore.Data.Models
{
    public class Issuer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string IndexName { get; set; }

        public ICollection<Ticker> Tickers { get; set; }
    }
}