using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StonksCore.Dto;

namespace StonksCore.Data.Repository
{
    public class IssuersRepository
    {
        private readonly StonksDbContext _context;

        public IssuersRepository(StonksDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IssuerDto>> GetAllIssuersAsync()
        {
            var tickers = await _context.Issuers.ToArrayAsync();

            return tickers.Select(x => x.ToDto());
        }

        public async Task<IssuerDto> AddIssuerAsync(IssuerDto issuerDto)
        {
            var issuer = issuerDto.FromDto();
            await _context.Issuers.AddAsync(issuer);
            await _context.SaveChangesAsync();
            return issuer.ToDto();
        }

        public async Task<IssuerDto> GetIssuerAsync(string name)
        {
            var issuer = await _context.Issuers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.IndexName == name.ToIndexName());
            return issuer?.ToDto();
        }
        public async Task<IssuerDto> GetIssuerAsync(int id)
        {
            var issuer = await _context.Issuers
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return issuer?.ToDto();
        }
    }
}