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
        public Task<TickerDto> GetTickerByIsinAsync(string isin)
        {
            
            return _service.GetTickerByIsinAsync(isin);
        }
        /// <summary>
        /// получение полной информации об активе по id
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("by-id/{id}")]
        public   Task<TickerDto> GetTickerByIdAsync(string id)
        {
            
            return _service. GetTickerByIdAsync( id);
        }

        /// <summary>
        /// поиск активов
        /// </summary>
        /// <param name="name">по части названия</param>
        /// <param name="id">части ticker'а</param>
        /// <param name="isin">части isin-кода</param>
        /// <param name="type">Тип актива</param>
        /*
    - в параметре запроса не менее 3 символов;
    - дополнительный необязательный параметр - класс актива (для выборки только по заданному классу);
    - результаты сгруппированы по классу актива;
    - только краткая информация.
         */
        [HttpGet("search")]
        public   Task<IEnumerable<TickerDto>> SearchAsync(string name, string id, string isin, TickerType[] type)
        {
            return _service.SearchAsync(name, id, isin, type);
        }
    }
}