using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Linq;
using WebApplicationFinal.Data.Interfaces;
using WebApplicationFinal.Data.Models;
using WebApplicationFinal.ViewModels;

namespace WebApplicationFinal.Controllers
{
    public class ShopCartController : Controller
    {
        private readonly IAllProduct _productRep;
        private readonly ShopCart _shopCart;


        public ShopCartController(IAllProduct productRep, ShopCart shopCart) {
            _productRep = productRep;
            _shopCart = shopCart;
        }

        public ViewResult Index()
        {
            ViewBag.Title = "Корзина - Енот и Панда";
            var items = _shopCart.getShopItems();
            _shopCart.listShopItems = items;

          
            var obj = new ShopCartViewModel
            {
                ShopCart = _shopCart
            };

            return View(obj);
        }

        public RedirectToActionResult addToCart(int id)
        {
            //проверяем id
            //var CheckId = _shopCart
            var item = _productRep.Product.FirstOrDefault(i => i.id == id);
            if (item != null)
            {
                _shopCart.AddToCart(item);
            }
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var item = _productRep.Product.FirstOrDefault(i => i.id == id);
            _shopCart.RemoveFromCart(id);
            return RedirectToAction("Index");
        }

        public IActionResult AddQuantity(int id)
        {
            var item = _productRep.Product.FirstOrDefault(i => i.id == id);
            _shopCart.AddQuantity(id);
            return RedirectToAction("Index");
        }

        public IActionResult DelQuantity(int id)
        {
            var item = _productRep.Product.FirstOrDefault(i => i.id == id);
            _shopCart.DelQuantity(id);
            return RedirectToAction("Index");
        }

        public IActionResult Charge(string stripeEmail, string stripeToken)
        {
            var customers = new CustomerService();
            var charges = new ChargeService();

            var customer = customers.Create(new CustomerCreateOptions
            {
                Email = stripeEmail,
                Source = stripeToken
            });

            var charge = charges.Create(new ChargeCreateOptions
            {
                Description = "TestPayment",
                Currency = "usd",
                Customer = customer.Id
            });

            if (charge.Status == "succeeded")
            {
                string BalanceTransactionId = charge.BalanceTransactionId;
                return View();
            }

            return View();
        }

    }
}
