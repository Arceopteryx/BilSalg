using BilSalg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace BilSalg.Controllers
{
    public class SaleController : Controller
    {
        // GET: Sale
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(Sale model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if(model.ImageURL == null)
            {
                model.ImageURL = "http://placehold.it/250x250";
            }

            model.ApplicationUserId = User.Identity.GetUserId();

            var db = new ApplicationDbContext();
            db.Sales.Add(model);
            db.SaveChanges();

            return RedirectToAction("Thanks");
        }

        //This displays the cars for sale, that belongs to the user that made the sales
        [Authorize]
        [HttpGet]
        public ActionResult DisplaySales()
        {
            string userid = User.Identity.GetUserId();

            var db = new ApplicationDbContext();

            IEnumerable<Sale> sales = from sale in db.Sales
                                      where sale.ApplicationUserId == userid
                                      select sale;

            var saleViewModel = new SalesViewModel(sales);

            return View(saleViewModel);
        }

        //This is gonna display all the cars for sale
        [HttpGet]
        public ActionResult CarsForSale()
        {
            //THIS NEEDS A MASSIVE SEPERATION OF CONCERNS FIX
            //REFACTOR
            //REFACTOR!!!!!!
            //REFACTOR!!!!!!!!!!!!!!!!!!!!!

            string userid = User.Identity.GetUserId();

            if (userid != null)
            {
                
                var db = new ApplicationDbContext();

                IEnumerable<Sale> sales = from sale in db.Sales
                                          where sale.ApplicationUserId != userid
                                          select sale;

                var saleViewModel = new SalesViewModel(sales);

                return View(saleViewModel);
            }
            else
            {
                var db = new ApplicationDbContext();

                IEnumerable<Sale> sales = from sale in db.Sales
                                          select sale;

                var saleViewModel = new SalesViewModel(sales);

                return View(saleViewModel);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult Remove(int id)
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Remove(RemoveViewModel model)
        {
            var db = new ApplicationDbContext();

            Sale sale = db.Sales.Single(x => x.Id == model.Id);

            db.Sales.Remove(sale);
            db.SaveChanges();

            return View("Removed");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var db = new ApplicationDbContext();

            Sale sale = db.Sales.Single(x => x.Id == id);

            string userid = User.Identity.GetUserId();

            if(userid != sale.ApplicationUserId)
            {
                Error();
            }

            return View(sale);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(Sale model)
        {
            if (!ModelState.IsValid)
            {
                Error();
            }

            var db = new ApplicationDbContext();

            Sale sale = db.Sales.Single(x => x.Id == model.Id);

            //sale = model did not work!!
            //This is not very good code. There must be a better fix.
            //I'll leave this as is, and change it if I find a solution

            sale.Variant = model.Variant;
            sale.Year = model.Year;
            sale.Engine = model.Engine;
            sale.Mileage = model.Mileage;
            sale.FirstName = model.FirstName;
            sale.LastName = model.LastName;
            sale.Phone = model.Phone;
            sale.Price = model.Price;

            db.SaveChanges();

            return View("Thanks");

        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Thanks()
        {
            return View();
        }
    }
}