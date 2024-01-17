using Lab3.Models;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab3.Controllers
{
    public class HomeController : Controller
    {
        private IValueCalculator _calc;
        private Product[] _products =
        {
            new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
            new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
            new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.56M},
            new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
        };
        public HomeController(IValueCalculator calcParam, IValueCalculator calc2)
        {
            _calc = calcParam;
        }
        // GET: Home
        public ActionResult Index()
        {
            ShoppingCart cart = new ShoppingCart(_calc) { Products = _products };
            decimal totalValue = cart.CalculateProductsTotal();
            return View(totalValue);
        }
    }
}