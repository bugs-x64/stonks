using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StonksCore.Data.Repository;
using StonksCore.Dto;
using StonksCore.Models;

namespace StonksCore.Services
{
    public class TickersService
    {
        private readonly TickersRepository _tickersRepository;

        public TickersService(TickersRepository tickersRepository)
        {
            _tickersRepository = tickersRepository;
        }

        public async Task<TickerDto> GetTickerByIsinAsync(string isin)
        {
            if (!IsValidParameter(isin))
                throw new Exception("Укажите параметры поиска");
            return await _tickersRepository.GetTickerByIsinAsync(isin);
        }

        public async Task<TickerDto> GetTickerByIdAsync(string tickerName)
        {
            if (!IsValidParameter(tickerName))
                throw new Exception("Укажите параметры поиска");
            return await _tickersRepository.GetTickerByIdAsync(tickerName);
        }

        public async Task<IEnumerable<SearchResultDto>> SearchAsync(string name, string ticker, string isin, TickerType[] types)
        {
            if (IsValidParameter(name) && IsValidParameter(ticker) && IsValidParameter(isin))
                throw new Exception("Укажите параметры поиска");

            if (types is null || !types.Any())
                types = Enum.GetValues<TickerType>();
            var tickerDtos = await _tickersRepository.SearchAsync(name, ticker, isin, types);

            return tickerDtos.GroupBy(x => x.Type, ((type, tickers) => new SearchResultDto()
            {
                Type = type,
                Tickers = tickers.ToArray()
            }));
        }

        public class SearchResultDto
        {
            public string Type { get; set; }
            public TickerDto[] Tickers { get; set; }
        }

        private static bool IsValidParameter(string value)
        {
            return !string.IsNullOrWhiteSpace(value) && value.Length > 3;
        }
    }
}