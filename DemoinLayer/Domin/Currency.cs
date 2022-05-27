using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoinLayer.Domin
{
    public class Currency : BaseCurrency
    {
        [Required]
        public string Name { get; set; }
        public string Sign { get; set; }
        public bool IsActive { get; set; }
        public List<ExchangeHistory> exchangeHistories { get; } = new List<ExchangeHistory>();
    }
}
