using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StonksCore.Data.Repository;
using StonksCore.Dto;

namespace StonksWebApi.Controllers
{
    [ApiController]
    [Route(RouteConstants.RpcRoute)]
    public class ToolsController : ControllerBase
    {
        private readonly IssuersRepository _issuersRepository;
        private readonly TickersRepository _tickersRepository;

        public ToolsController(TickersRepository tickersRepository, IssuersRepository issuersRepository)
        {
            _tickersRepository = tickersRepository;
            _issuersRepository = issuersRepository;
        }


        /// <summary>
        /// Заполнить базу тестовыми данными
        /// </summary>
        [HttpPost]
        public async Task FillData()
        {
            var tickerDtos = (await ReadTickerDtos("stocks.json"))
                .Union(await ReadTickerDtos("bonds.json"));
            if (tickerDtos is null)
                throw new Exception(nameof(tickerDtos));

            var issuers = tickerDtos.GroupBy(x => x.Name,
                (issuer, tickers) => new {issuer, tickers});

            foreach (var issuerData in issuers)
            {
                var name = issuerData.issuer;
                var issuer = await _issuersRepository.GetIssuerAsync(name);
                if (issuer is null)
                {
                    issuer = await _issuersRepository.AddIssuerAsync(new IssuerDto
                    {
                        Name = name,
                    });
                }

                foreach (var ticker in issuerData.tickers)
                {
                    ticker.IssuerId = issuer.Id;
                    ticker.OnMarketFrom = GetRandomDate();
                }
            }

            const int maxCount = 10;
            var tickersCount = tickerDtos.Count();
            var allIssuers = await _issuersRepository.GetAllIssuersAsync();
            var sber = allIssuers.First(x => x.Name.ToLower().Contains("сбер"));
            var nornikel = allIssuers.First(x => x.Name.ToLower().Contains("норильский никель"));
            var sberIds= Enumerable
                .Range(0, maxCount)
                .Select(x=>new Random().Next(0,tickersCount));
            var nornikelIds= Enumerable
                .Range(0, maxCount)
                .Select(x=>new Random().Next(0,tickersCount));
            var id = 0;
            foreach (var ticker in tickerDtos)
            {
                id++;
                if (sberIds.Contains(id))
                    ticker.IssuerId = sber.Id;
                if (nornikelIds.Contains(id))
                    ticker.IssuerId = nornikel.Id;
                
                var tickerById = await _tickersRepository.GetTickerByIdAsync(ticker.Ticker);
                if (tickerById is null)
                    await _tickersRepository.AddTickerAsync(ticker);
            }
        }

        private static DateTime GetRandomDate()
        {
            return DateTime.UtcNow.AddDays(-new Random().Next(0, 3600));
        }

        private static async Task<TickerDto[]> ReadTickerDtos(string filename)
        {
            var tickerJosn = await System.IO.File.ReadAllLinesAsync(filename);
            using var textReader = new StringReader(string.Join(Environment.NewLine, tickerJosn));
            using var reader = new JsonTextReader(textReader);
            return new JsonSerializer().Deserialize<TickerDto[]>(reader);
        }

        [HttpPost]
        public Task<TickerDto> AddTicker(TickerDto dto)
        {
            return _tickersRepository.AddTickerAsync(dto);
        }

        [HttpPost]
        public Task<IssuerDto> AddIssuer(IssuerDto dto)
        {
            return _issuersRepository.AddIssuerAsync(dto);
        }
        
        [HttpPost]
        public async Task<TickerDto> LinkTickerToCompany(string tickerName, int issuerId)
        {
            var ticker = await _tickersRepository.GetTickerByIdAsync(tickerName);
            if (ticker is null)
                throw new Exception("неправильный tickerName");

            var issuer = await _issuersRepository.GetIssuerAsync(issuerId);
            if (issuer is null)
                throw new Exception("неправильный issuerId");
            
            
            ticker.IssuerId = issuer.Id;
            
            return await _tickersRepository.UpdateTickerAsync(ticker);
        }
    }
}