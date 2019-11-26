using Microsoft.AspNetCore.Mvc;
using System.Linq;
using WebApplicationFinal.Data.Interfaces;
using WebApplicationFinal.Data.Models;
using WebApplicationFinal.ViewModels;

namespace WebApplicationFinal.Controllers
{
    public class ShopCartController : Controller
    {
        private readonly IAllCars _carRep;
        private readonly ShopCart _shopCart;
        private readonly ShopCartItem _shopCartItem;

        public ShopCartController(IAllCars carRep, ShopCart shopCart) {
            _carRep = carRep;
            _shopCart = shopCart;
        }

        public ViewResult Index()
        {
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
            var item = _carRep.Cars.FirstOrDefault(i => i.id == id);
            if (item != null) {
                _shopCart.AddToCart(item);
            }
            return RedirectToAction("Index");
        }

        public RedirectToActionResult removeFromCart(int id)
        {
            var item = _carRep.Cars.FirstOrDefault(i => i.id == id);
            ViewBag.Title = "id: " + id;
            //_shopCart.RemoveFromCart(item);

            return RedirectToAction("Index");
        }
    }
}
