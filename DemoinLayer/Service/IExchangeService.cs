using CurrencyConverter.ViewModel;
using DemoinLayer.Domin;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoinLayer.Service
{
    public interface IExchangeService : IBaseService<ExchangeHistory>
    {
        public IEnumerable<ChangeRate> GetMostNImprovedCurrenciesByDate(DateTime FirstDate, DateTime EndDate, int N);

        public IEnumerable<ChangeRate> GetLeastNImprovedCurrenciesByDate(DateTime FirstDate, DateTime EndDate, int N);
    }
}
