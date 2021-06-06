using System.Collections.Generic;
using System.Threading.Tasks;
using StonksCore.Dto;
using StonksCore.Models;

namespace StonksCore.Services
{
    public class TickersService
    {
        public Task<TickerDto> GetTickerByIsinAsync(string isin)
        {
            throw new System.NotImplementedException();
        }

        public Task<TickerDto> GetTickerByIdAsync(string id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<TickerDto>> SearchAsync(string name, string id, string isin, TickerType[] type)
        {
            throw new System.NotImplementedException();
        }
    }
}