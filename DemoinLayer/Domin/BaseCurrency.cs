using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DemoinLayer.Domin
{
    public abstract class BaseCurrency
    {
        public int Id { get; set; }
        
        [Required]
        public double Rate { get; set; }
    }
}
