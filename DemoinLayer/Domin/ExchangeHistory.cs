using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoinLayer.Domin
{
    public class ExchangeHistory : BaseCurrency
    {
        [Required]
        public DateTime EexchandeDate { get; set; } 

        public int CurrencyId { get; set; }
        public Currency Currency { get; set; }
    }
}
