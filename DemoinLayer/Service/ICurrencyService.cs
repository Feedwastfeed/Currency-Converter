using DemoinLayer.Domin;
using System;
using System.Collections.Generic;
using System.Text;

namespace DemoinLayer.Service
{
    public interface ICurrencyService : IBaseService<Currency>
    {
        Currency GetByName(string name);
        public List<Currency> GetHighestNCurrencies(int N);
        public List<Currency> GetLowestNCurrencies(int N);
        public double ConvertAmount(string FromCurrency, string ToCurrency, double Amount);
    }
}
