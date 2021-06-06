using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StonksCore.Dto;
using StonksCore.Models;

namespace StonksCore.Data.Repository
{
    public class TickersRepository
    {
        private readonly StonksDbContext _context;

        public TickersRepository(StonksDbContext context)
        {
            _context = context;
        }

        public async Task<TickerDto> GetTickerByIdAsync(string tickerName)
        {
            var ticker = await _context.Tickers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TickerName == tickerName.ToIndexName());

            return ticker?.ToDto();
        }

        public async Task<IEnumerable<TickerDto>> GetIssuerTickersAsync(int issuerId)
        {
            var tickers = await _context.Tickers
                .AsNoTracking()
                .Where(x => x.IssuerId == issuerId).ToArrayAsync();
            return tickers?.Select(x => x.ToDto());
        }

        public async Task<TickerDto> AddTickerAsync(TickerDto tickerDto)
        {
            var ticker = tickerDto.FromDto();
            await _context.Tickers.AddAsync(ticker);
            await _context.SaveChangesAsync();
            return ticker.ToDto();
        }

        public async Task<TickerDto> GetTickerByIsinAsync(string isin)
        {
            var ticker = await _context.Tickers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Isin == isin.ToIndexName());

            return ticker?.ToDto();
        }

        public async Task<IEnumerable<TickerDto>> SearchAsync(string name, string ticker, string isin, TickerType[] types)
        {
            //SQLite Не умеет искать не ASCII символы, поэтому вычисляем в приложении
            var tickers = _context.Tickers
                .AsNoTracking()
                .ToArray()
                .Where(x =>
                    ((!ticker.IsNullOrEmpty() && x.TickerName.ToIndexName().Contains(ticker.ToIndexName()))
                     || (!isin.IsNullOrEmpty() && x.Isin.ToIndexName().Contains(isin.ToIndexName()))
                     || (!name.IsNullOrEmpty() && x.Name.ToIndexName().Contains(name.ToIndexName())))
                    && types.Contains(x.Type));

            return tickers?
                .OrderByDescending(x=>x.OnMarketFrom)
                .Select(x => x.ToDto());
        }

        public async Task<TickerDto> UpdateTickerAsync(TickerDto ticker)
        {
            var entity = ticker.FromDto();
            _context.Tickers.Update(entity);

            await _context.SaveChangesAsync();
            return entity.ToDto();
        }
    }
}