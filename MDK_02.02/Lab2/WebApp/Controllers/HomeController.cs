using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController:Controller
    {
        public ViewResult Index()
        {
            ViewBag.Title = "MyCafe";
            return View();
        }
        [HttpGet]
        public ViewResult DrinkForm()
        {
            ViewBag.Title = "Drinks";
            ViewBag.AAAAAAAA = "sssssssss";
            return View();
        }
        [HttpPost]
        public ViewResult DrinkForm(DrinkData drinkData)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Title = "Bill";
                //

                ViewBag.Cost = 150;
                return View("Bill", drinkData);
            }
            else
                return View();
        }

        public ViewResult Mehtod1(string page, string pageSize)
        {
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            return View();
        }
    }
}