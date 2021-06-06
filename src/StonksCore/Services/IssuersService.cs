using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StonksCore.Data.Repository;
using StonksCore.Dto;

namespace StonksCore.Services
{
    public class IssuersService
    {
        private readonly IssuersRepository _issuersRepository;
        private readonly TickersRepository _tickersRepository;

        public IssuersService(IssuersRepository issuersRepository, TickersRepository tickersRepository)
        {
            _issuersRepository = issuersRepository;
            _tickersRepository = tickersRepository;
        }

        public Task<IEnumerable<IssuerDto>> GetAllIssuersAsync()
        {
            return _issuersRepository.GetAllIssuersAsync();
        }

        public Task<IEnumerable<TickerDto>> GetIssuersTickersAsync(int issuerId)
        {
            if (issuerId <= 0)
                throw new Exception("Укажите параметры поиска");
            
            return _tickersRepository.GetIssuerTickersAsync(issuerId);
        }
    }
}