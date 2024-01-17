using Lab4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lab4.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        public CartController(IProductRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public PartialViewResult Summary(Cart cart)
        {
            return PartialView(cart);
        }
        [HttpGet]
        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }
        [HttpPost]
        public ViewResult Checkout(Cart cart, Order order, dynamic GiftWrap)
        {
            if (GiftWrap.GetType() == typeof(string[]) &&
                GiftWrap.Length>0 && GiftWrap[0] == "true")
                order.GiftWrap = true;
            //cart.Clear(
            MakeOrder(order);
            AddProductsToOrder(cart, order.OrderID);
            ViewBag.cartLines = cart.Lines;
            Tuple<string, int> t1 = new Tuple<string, int>("hjhj", 78);
            cart.RemoveAllLines();
            EFDbContext context = new EFDbContext();
            return View("AfterOrdered",new AfterOrderedModel(cart, order));
        }
        private void MakeOrder(Order order)
        {
            EFDbContext context = new EFDbContext();
            context.Order.Add(order);
            context.SaveChanges();
        }
        private void AddProductsToOrder(Cart cart,int orderID)
        {
            EFDbContext context = new EFDbContext();
            foreach (var line in cart.Lines)
            {
                context.ProductsForOrders.Add(new ProductsForOrders(line, orderID));
            }
            context.SaveChanges();
        }
    }
}