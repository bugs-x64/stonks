using System.Collections.Generic;
using System.Threading.Tasks;
using StonksCore.Dto;

namespace StonksCore.Services
{
    public class IssuersService
    {
        public Task<IEnumerable<IssuerDto>> GetAllIssuersAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<TickerDto>> GetIssuersTickersAsync(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}