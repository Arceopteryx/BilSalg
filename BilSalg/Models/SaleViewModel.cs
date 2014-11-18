using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BilSalg.Models
{
    public class SaleViewModel
    {
        public Sale MySale { get; set; }

        public SaleViewModel(Sale sale)
        {
            this.MySale = sale;
        }
    }
}