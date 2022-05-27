using CurrencyConverter.ViewModel;
using DemoinLayer.Domin;
using DemoinLayer.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;
        private readonly IExchangeService _exchangeService;

        public OperationsController(ICurrencyService currencyService , IExchangeService exchangeService)
        {
            this._currencyService = currencyService;
            this._exchangeService = exchangeService;
        }

        [HttpGet("GetHighestNCurrencies")]
        public IEnumerable<Currency> GetHighestNCurrencies(int N)
        {
            return _currencyService.GetHighestNCurrencies(N);
        }

        [HttpGet("GetLowestNCurrencies")]
        public IEnumerable<Currency> GetLowestNCurrencies(int N)
        {
            return _currencyService.GetLowestNCurrencies(N);
        }

        [HttpGet("Convert")]
        public ActionResult<double> ConvertAmount(string FromCurrency , string ToCurrency , double Amount)
        {
            var cur1 = _currencyService.GetByName(FromCurrency);
            var cur2 = _currencyService.GetByName(ToCurrency);
            if (cur1 == null || cur2 == null)
                return NotFound("this currency not found");
            return _currencyService.ConvertAmount(FromCurrency, ToCurrency, Amount);
        }

        [HttpGet("GetLeastNImprovedCurrenciesByDate")]
        public ActionResult<IEnumerable<ChangeRate>> GetLeastNImprovedCurrenciesByDate(DateTime StartDate , DateTime EndDate , int N)
        {
            if (DateTime.Compare(StartDate, EndDate) > 0)
            {
                return NotFound("Wrong date");
            }

            var changeRates = _exchangeService.GetLeastNImprovedCurrenciesByDate(StartDate, EndDate, N);

            if(changeRates == null ||  changeRates.Count() == 0)
            {
                return NotFound("No currencies changed");
            }
            return Ok(changeRates);
        }

        [HttpGet("GetMostNImprovedCurrenciesByDate")]
        public ActionResult<IEnumerable<ChangeRate>> GetMostNImprovedCurrenciesByDate(DateTime StartDate, DateTime EndDate, int N)
        {
            if(DateTime.Compare(StartDate , EndDate ) > 0)
            {
                return NotFound("Wrong date");
            }

            var changeRates = _exchangeService.GetMostNImprovedCurrenciesByDate(StartDate, EndDate, N);

            if (changeRates == null || changeRates.Count() == 0)
            {
                return NotFound("No currencies changed");
            }
            return Ok(changeRates);
        }
    }
}
