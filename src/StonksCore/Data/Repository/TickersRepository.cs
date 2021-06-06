using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StonksCore.Dto;
using StonksCore.Services;

namespace StonksCore.Data.Repository
{
    public class TickersRepository
    {
        private readonly StonksDbContext _context;

        public TickersRepository(StonksDbContext context)
        {
            _context = context;
        }

        public async Task<TickerDto> GetTickerById(string tickerName)
        {
            var ticker = await _context.Tickers.FirstOrDefaultAsync(x => x.TickerName == tickerName);

            return ticker?.ToDto();
        }
        
        public TickerDto GetTickerByIsin(string isin)
        {
            
            throw new System.NotImplementedException();
        }
        public TickerDto[] GetIssuerTickers(int id)
        {
            throw new System.NotImplementedException();
            
        }

        public async Task<TickerDto> AddTicker(TickerDto tickerDto)
        {
            var ticker = tickerDto.FromDto(); 
            await _context.Tickers.AddAsync(ticker);
            await _context.SaveChangesAsync();
            return ticker.ToDto();
        }
    }
}