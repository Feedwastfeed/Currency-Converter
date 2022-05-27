using CurrencyConverter.ViewModel;
using DemoinLayer.Domin;
using DemoinLayer.Repository;
using DemoinLayer.Service;
using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class ExchangeService : BaseService<ExchangeHistory>, IExchangeService
    {
        private readonly IRepository<ExchangeHistory> _repository;
        private readonly ICurrencyService _currencyService;
        public ExchangeService(IRepository<ExchangeHistory> repository, ICurrencyService currencyService) : base(repository)
        {
            this._repository = repository;
            this._currencyService = currencyService;
        }


        public IEnumerable<ChangeRate> GetMostNImprovedCurrenciesByDate(DateTime FirstDate , DateTime EndDate, int N)
        {
            IEnumerable<ChangeRate> changeRates = GetChangeRateInSpecifiedDate(FirstDate , EndDate);
            return changeRates.Where(e => e.changeRate > 0).OrderByDescending(e => e.changeRate).Take(N);
        }

        public IEnumerable<ChangeRate> GetLeastNImprovedCurrenciesByDate(DateTime FirstDate, DateTime EndDate, int N)
        {
            IEnumerable<ChangeRate> changeRates = GetChangeRateInSpecifiedDate(FirstDate, EndDate);
            return changeRates.Where(e => e.changeRate < 0).OrderBy(e => e.changeRate).Take(N);
        }


        private IEnumerable<ChangeRate> GetChangeRateInSpecifiedDate(DateTime Firstdate , DateTime EndDate)
        {
            List<ChangeRate> changeRates = new List<ChangeRate>();
            DateTime dateTime = GetFirstDate();

            Firstdate = Firstdate.AddDays(-1);

            var listOfIds = _currencyService.GetAll().Where(e => e.IsActive == true).Select(e => e.Id);
            foreach(var id in listOfIds)
            {
                double startRate = GetPreviousRate(Firstdate, dateTime, id);
                if(startRate == -1)
                {
                    startRate = GetNextRate(Firstdate, EndDate, id);
                }
                if (startRate == -1)
                    continue;
                double endRate = GetPreviousRate(EndDate, dateTime, id);
                if (startRate != endRate) {
                    changeRates.Add(new ChangeRate
                    {
                        currency = _currencyService.Get(id),
                        changeRate = endRate - startRate
                    });
                }
            }
            return changeRates;
        }

        private double GetPreviousRate(DateTime startdate , DateTime EndDate , int id)
        {
            while(DateTime.Compare(startdate , EndDate) >= 0)
            {
                try
                {
                    var res = GetAll().Where(e => e.CurrencyId == id &&
                              DateTime.ParseExact(e.EexchandeDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null) == startdate)
                              .Max(e => e.EexchandeDate);
                    var newres = GetAll().Where(e => e.CurrencyId == id && e.EexchandeDate == res).Select(e => e.Rate).First();
                    return newres;
                }
                catch (InvalidOperationException e)
                {
                    startdate = startdate.AddDays(-1);
                }
            }
            return -1;
        }

        private double GetNextRate(DateTime startdate, DateTime EndDate, int id)
        {
            while (DateTime.Compare(startdate, EndDate) <= 0)
            {
                try
                {
                    var res = GetAll().Where(e => e.CurrencyId == id &&
                              DateTime.ParseExact(e.EexchandeDate.ToString("dd/MM/yyyy"), "dd/MM/yyyy", null) == startdate)
                             .Min(e => e.EexchandeDate);
                    var newres = GetAll().Where(e => e.CurrencyId == id && e.EexchandeDate == res).Select(e => e.Rate).First();
                    return newres;
                }
                catch (InvalidOperationException e)
                {
                    startdate = startdate.AddDays(1);
                }
            }
            return -1;
        }

        private DateTime GetFirstDate()
        {
            DateTime dateTime = GetAll().Select(s => s.EexchandeDate).Min();
            dateTime = DateTime.ParseExact(dateTime.ToString("dd/MM/yyyy") , "dd/MM/yyyy" , null);
            return dateTime;
        }
    }
}
