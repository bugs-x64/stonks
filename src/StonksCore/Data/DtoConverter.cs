using System;
using StonksCore.Data.Models;
using StonksCore.Dto;
using StonksCore.Models;

namespace StonksCore.Services
{
    public static class DtoConverter
    {
        public static TickerDto ToDto(this Ticker ticker)
        {
            return new ()
            {
                Currency = ticker.Currency,
                Figi = ticker.Figi,
                Isin = ticker.Isin,
                Lot = ticker.Lot,
                Name = ticker.Name,
                Ticker = ticker.TickerName,
                Type = ticker.Type.ToString(),
                MinPriceIncrement = ticker.MinPriceIncrement
            };
        }
        
        public static Ticker FromDto(this TickerDto ticker)
        {
            return new ()
            {
                Currency = ticker.Currency,
                Figi = ticker.Figi,
                Isin = ticker.Isin,
                Lot = ticker.Lot,
                Name = ticker.Name,
                TickerName = ticker.Ticker,
                Type = FromString(ticker.Type),
                MinPriceIncrement = ticker.MinPriceIncrement,
                IssuerId = ticker.IssuerId
            };
        }
        
        public static IssuerDto ToDto(this Issuer issuer)
        {
            return new ()
            {
                Id = issuer.Id,
                Name = issuer.Name
            };
        }

        public static TickerType FromString(string name)
        {
            name = name.ToLowerInvariant();
            return name switch
            {
                "bond" => TickerType.Bond,
                "stock" => TickerType.Stock,
                _ => throw new ArgumentOutOfRangeException(nameof(name))
            };
        }

        public static Issuer FromDto(this IssuerDto issuer)
        {
            return new ()
            {
                Id = issuer.Id,
                Name = issuer.Name,
                IndexName = ToIndexName(issuer.Name)
            };
        }

        public static string ToIndexName(string name) => 
            name.ToLower().Replace(" ", "");
    }
}