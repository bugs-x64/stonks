using System;
using StonksCore.Data.Models;
using StonksCore.Dto;
using StonksCore.Models;

namespace StonksCore.Data
{
    public static class DtoConverter
    {
        public static TickerDto ToDto(this Ticker ticker)
        {
            return new()
            {
                Currency = ticker.Currency,
                Figi = ticker.Figi,
                Isin = ticker.Isin,
                Lot = ticker.Lot,
                Name = ticker.Name,
                Ticker = ticker.TickerName,
                Type = ticker.Type.ToString(),
                MinPriceIncrement = ticker.MinPriceIncrement,
                IssuerId = ticker.IssuerId,
                OnMarketFrom = ticker.OnMarketFrom
            };
        }

        public static Ticker FromDto(this TickerDto ticker)
        {
            return new()
            {
                Currency = ticker.Currency,
                Figi = ticker.Figi,
                Isin = ticker.Isin,
                Lot = ticker.Lot,
                Name = ticker.Name,
                TickerName = ticker.Ticker.ToIndexName(),
                Type = FromString(ticker.Type),
                MinPriceIncrement = ticker.MinPriceIncrement,
                IssuerId = ticker.IssuerId,
                OnMarketFrom = ticker.OnMarketFrom
            };
        }

        public static IssuerDto ToDto(this Issuer issuer)
        {
            return new()
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
            return new()
            {
                Id = issuer.Id,
                Name = issuer.Name,
                IndexName = issuer.Name.ToIndexName()
            };
        }

        public static string ToIndexName(this string name) =>
            name?.ToUpperInvariant().Replace(" ", "");
    }
}