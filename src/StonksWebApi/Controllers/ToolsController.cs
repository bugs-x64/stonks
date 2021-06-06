using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StonksCore.Data.Repository;
using StonksCore.Dto;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace StonksWebApi.Controllers
{
    [ApiController]
    [Route(RouteConstants.RpcRoute)]
    public class ToolsController : ControllerBase
    {
        private readonly TickersRepository _tickersRepository;
        private readonly IssuersRepository _issuersRepository;

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
                var issuer = await _issuersRepository.GetIssuer(name);
                if (issuer is null)
                {
                    issuer = await _issuersRepository.AddIssuer(new IssuerDto()
                    {
                        Name = name,
                    });
                }

                foreach (var ticker in issuerData.tickers)
                {
                    ticker.IssuerId = issuer.Id;
                }
            }

            foreach (var ticker in tickerDtos)
            {
                var tickerById = await _tickersRepository.GetTickerById(ticker.Ticker);
                if (tickerById is null)
                    await _tickersRepository.AddTicker(ticker);
            }
        }

        private static async Task<TickerDto[]> ReadTickerDtos(string filename)
        {
            var tickerJosn = await System.IO.File.ReadAllLinesAsync(filename);
            using var textReader = new StringReader(string.Join(Environment.NewLine, tickerJosn));
            using var reader = new JsonTextReader(textReader);
            var jsonSerializer = new JsonSerializer();
            var tickerDtos = jsonSerializer.Deserialize<TickerDto[]>(reader);
            return tickerDtos;
        }

        [HttpPost]
        public Task<TickerDto> AddTicker(TickerDto dto)
        {
            return _tickersRepository.AddTicker(dto);
        }

        [HttpPost]
        public Task<IssuerDto> AddIssuer(IssuerDto dto)
        {
            return _issuersRepository.AddIssuer(dto);
        }
    }
}