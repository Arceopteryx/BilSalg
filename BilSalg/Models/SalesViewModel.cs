using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BilSalg.Models
{
    public class SalesViewModel
    {
        public IEnumerable<Sale> MySale { get; set; }

        public SalesViewModel(IEnumerable<Sale> sale)
        {
            MySale = sale;
        }
    }
}