using DemoinLayer.Domin;
using DemoinLayer.Repository;
using DemoinLayer.Service;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLayer
{
    public class CurrencyService : BaseService<Currency>, ICurrencyService
    {
        private readonly IRepository<Currency> _repository;

        public CurrencyService(IRepository<Currency> repository) : base(repository)
        {
            this._repository = repository;
        }

        public Currency GetByName(string name)
        {
            IEnumerable<Currency>  currencies =  GetAll();
            foreach(Currency cur in currencies)
            {
                if (cur.Name == name && cur.IsActive == true)
                    return cur;
            }
            return null;
        }

        public List<Currency> GetHighestNCurrencies(int N)
        {
           return GetAll().Where(e => e.IsActive == true).OrderByDescending(o => o.Rate).Take(N).ToList();
        }

        public List<Currency> GetLowestNCurrencies(int N)
        {
            return GetAll().Where(e => e.IsActive == true).OrderBy(o => o.Rate).Take(N).ToList();
        }

        public double  ConvertAmount(string FromCurrency, string ToCurrency, double Amount)
        {
            
            double FromCurrencyRate = GetAll().Where(e => e.Name == FromCurrency).Select(s => s.Rate).First();
            double ToCurrencyRate = GetAll().Where(e => e.Name == ToCurrency).Select(s => s.Rate).First();

            return  Amount * FromCurrencyRate / ToCurrencyRate;
        }
    }
}
