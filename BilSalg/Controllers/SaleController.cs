using BilSalg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using BilSalg.Repositories;

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

            SaleRepository.SaveSale(model);

            return RedirectToAction("Thanks");
        }

        //This displays the cars for sale, that belongs to the user that made the sales
        [Authorize]
        [HttpGet]
        public ActionResult DisplaySales()
        {
            return View(SaleRepository.DisplayUserSales(User.Identity.GetUserId()));
        }

        //This is gonna display all the cars for sale
        [HttpGet]
        public ActionResult CarsForSale()
        {
            return View(SaleRepository.DisplayCarsForSale());
        }

        [Authorize]
        [HttpGet]
        public ActionResult Remove(int id)
        {
            Sale sale = SaleRepository.FetchSale(id);

            if (User.Identity.GetUserId() != sale.ApplicationUserId)
            {
                return View("Error");
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Remove(RemoveViewModel model)
        {
            SaleRepository.RemoveSale(model.Id);

            return View("Removed");
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Sale sale = SaleRepository.FetchSale(id);

            if(User.Identity.GetUserId() != sale.ApplicationUserId)
            {
                return View("Error");
            }

            return View(sale);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(Sale model)
        {
            if (!ModelState.IsValid)
            {
                return View("Error");
            }

            SaleRepository.EditSale(model);

            return View("Thanks");

        }

        public ActionResult Thanks()
        {
            return View();
        }
    }
}