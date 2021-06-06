using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StonksCore.Dto;
using StonksCore.Services;

namespace StonksCore.Data.Repository
{
    public class IssuersRepository
    {
        private readonly StonksDbContext _context;

        public IssuersRepository(StonksDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<IssuerDto>> GetAllIssuers()
        {
            throw new System.NotImplementedException();
        }

        public async Task<IssuerDto> AddIssuer(IssuerDto issuerDto)
        {
            var issuer = issuerDto.FromDto();
            await _context.Issuers.AddAsync(issuer);

            await _context.SaveChangesAsync();
            return issuer.ToDto();
        }

        public async Task<IssuerDto> GetIssuer(string name)
        {
            var indexName = DtoConverter.ToIndexName(name);
            var issuer = await _context.Issuers.FirstOrDefaultAsync(x => x.IndexName == indexName);
            return issuer?.ToDto();
        }
    }
}