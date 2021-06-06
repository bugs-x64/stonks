using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StonksCore.Dto;
using StonksCore.Models;
using StonksCore.Services;

namespace StonksWebApi.Controllers
{
    [ApiController]
    [Route(RouteConstants.RestRoute)]
    public class TickersController : ControllerBase
    {
        private readonly TickersService _service;

        public TickersController(TickersService service)
        {
            _service = service;
        }

        /// <summary>
        /// получение полной информации об активе по isin-коду
        /// </summary>
        /// <param name="isin"></param>
        [HttpGet("by-isin/{isin}")]
        public Task<TickerDto> GetTickerByIsinAsync([FromRoute] string isin)
        {
            return _service.GetTickerByIsinAsync(isin);
        }

        /// <summary>
        /// получение полной информации об активе по id
        /// </summary>
        /// <param name="ticker"></param>
        [HttpGet("by-ticker/{ticker}")]
        public Task<TickerDto> GetTickerByIdAsync([FromRoute] string ticker)
        {
            return _service.GetTickerByIdAsync(ticker);
        }

        /// <summary>
        /// поиск активов
        /// </summary>
        [HttpGet("search")]
        public Task<IEnumerable<TickersService.SearchResultDto>> SearchAsync([FromQuery] SearchDto requestDto)
        {
            return _service.SearchAsync(requestDto.Name, requestDto.Ticker, requestDto.Isin, requestDto.Types);
        }

        public class SearchDto
        {
            /// <summary>
            /// Поиск по имени актива
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// Поиск по isin
            /// </summary>
            public string Isin { get; set; }
            /// <summary>
            /// Поиск по id актива
            /// </summary>
            public string Ticker { get; set; }
            /// <summary>
            /// Типы активов
            /// </summary>
            public TickerType[] Types { get; set; }
        }
    }
}