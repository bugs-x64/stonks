using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StonksCore.Dto;
using StonksCore.Services;

namespace StonksWebApi.Controllers
{
    [ApiController]
    [Route(RouteConstants.RestRoute)]
    public class IssuersController : ControllerBase
    {
        private readonly IssuersService _service;

        public IssuersController(IssuersService service)
        {
            _service = service;
        }

        /// <summary>
        /// получение перечня всех эмитентов
        /// </summary>
        [HttpGet]
        public Task<IEnumerable<IssuerDto>> GetAllIssuersAsync()
        {
            return _service.GetAllIssuersAsync();
        }

        /// <summary>
        /// получение перечня всех активов эмитента по его id
        /// </summary>
        /// <param name="issuerId">id эмитента</param>
        /// <remarks>
        /// - единый перечень
        /// - без группировки
        /// - только доступные для покупки на бирже активы;
        /// - упорядочен по дате начала обращения;
        /// - только краткая информация.
        /// </remarks>
        [HttpGet("{issuerId}/tickers")]
        public Task<IEnumerable<TickerDto>> GetIssuersTickersAsync(int issuerId)
        {
            return _service.GetIssuersTickersAsync(issuerId);
        }
    }
}