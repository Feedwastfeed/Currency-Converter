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
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            this._currencyService = currencyService;
        }

        [HttpGet]
        public IEnumerable<Currency> Get()
        {
            return _currencyService.GetAll();
        }

        [HttpGet("getByName")]
        public ActionResult<Currency> GetByName (string Name)
        {
            Currency item =  _currencyService.GetByName(Name);
            if (item == null)
                return NotFound("this currency not found");
            return item;
        }

        [HttpPost]
        public ActionResult<string> AddCurrency (Currency currency)
        {
            Currency cur = _currencyService.GetByName(currency.Name);
            if(cur == null)
            {
                currency.exchangeHistories.Add(new ExchangeHistory
                {
                    EexchandeDate = DateTime.Now,
                    CurrencyId = currency.Id,
                    Rate = currency.Rate

                });
                _currencyService.Insert(currency);
                return Ok();
            }
            return "This Currency Added befor";
        }

        [HttpDelete]
        public ActionResult<string> DeleteCurrency(string NameOfCurrency)
        {
            Currency cur = _currencyService.GetByName(NameOfCurrency);
            if (cur != null)
            {
                _currencyService.Delete(cur);
                return Ok();
            }
            return NotFound("This Currency Not Found");
        }

        [HttpPut]
        public ActionResult<string> UpdateCurrency(Currency currency)
        {
            Currency cur = _currencyService.GetByName(currency.Name);
            if (cur != null)
            {
                cur.IsActive = currency.IsActive; 

                // if update currency isActive false do not update Rate

                if (currency.IsActive)
                {
                    cur.Rate = currency.Rate;
                    cur.Sign = currency.Sign;
                    cur.exchangeHistories.Add(new ExchangeHistory
                    {
                        EexchandeDate = DateTime.Now,
                        CurrencyId = currency.Id,
                        Rate = currency.Rate

                    });
                }
                _currencyService.Update(cur);
                return Ok();
            }
            return NotFound("This Currency Not Found");
        }
    }
}
