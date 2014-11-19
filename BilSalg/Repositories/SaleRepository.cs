using BilSalg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace BilSalg.Repositories
{
    public class SaleRepository
    {
        public static void SaveSale(Sale sale)
        {
            var db = new ApplicationDbContext();
            db.Sales.Add(sale);
            db.SaveChanges(); 
        }

        public static SalesViewModel DisplayUserSales(String userId)
        {

            var db = new ApplicationDbContext();

            IEnumerable<Sale> sales = from sale in db.Sales
                                      where sale.ApplicationUserId == userId
                                      select sale;

            return new SalesViewModel(sales);
        }

        public static SalesViewModel DisplayCarsForSale()
        {

            var db = new ApplicationDbContext();

            if(HttpContext.Current.User.Identity.IsAuthenticated)
            {
                string user = HttpContext.Current.User.Identity.GetUserId();

                IEnumerable<Sale> nonUserSales = from sale in db.Sales
                                                 where sale.ApplicationUserId != user
                                                 orderby sale.Variant
                                                 select sale;

                return new SalesViewModel(nonUserSales);
            }

            IEnumerable<Sale> allSales = from sale in db.Sales
                                         select sale;

            return new SalesViewModel(allSales);
        }

        public static void RemoveSale(int SaleId)
        {
            var db = new ApplicationDbContext();

            Sale sale = db.Sales.Single(x => x.Id == SaleId);

            db.Sales.Remove(sale);
            db.SaveChanges();
        }

        public static Sale FetchSale(int SaleId)
        {
            var db = new ApplicationDbContext();
            Sale sale = db.Sales.Single(x => x.Id == SaleId);

            return sale;
        }

        public static void EditSale(Sale model)
        {
            var db = new ApplicationDbContext();

            Sale sale = db.Sales.Single(x => x.Id == model.Id);

            //There's a little bad practice in all projects. Here's my part :(

            sale.Variant = model.Variant;
            sale.Year = model.Year;
            sale.Engine = model.Engine;
            sale.Mileage = model.Mileage;
            sale.FirstName = model.FirstName;
            sale.LastName = model.LastName;
            sale.Phone = model.Phone;
            sale.Price = model.Price;

            db.SaveChanges();
        }
    }
}