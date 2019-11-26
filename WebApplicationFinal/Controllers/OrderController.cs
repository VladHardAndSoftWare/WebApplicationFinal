using Microsoft.AspNetCore.Mvc;
using WebApplicationFinal.Data.Interfaces;
using WebApplicationFinal.Data.Models;

namespace WebApplicationFinal.Controllers
{
    public class OrderController : Controller
    {
        private readonly IAllOrder allOrders;
        private readonly ShopCart shopCart;

        public OrderController(IAllOrder allOrders, ShopCart shopCart)
        {
            this.allOrders = allOrders;
            this.shopCart = shopCart;
        }

        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]//принимает post запрос от Checkout.cshtml
        public IActionResult Checkout(Order order) {
            
            shopCart.listShopItems = shopCart.getShopItems();//берём все товары из корзины и встраиваем их в список

            if (shopCart.listShopItems.Count == 0) {//выполняем если ничего нет в корзине
                ModelState.AddModelError("", "У вас должны быть товары!");//выдача ошибки при отутсттвии товаров в корзине
            }
            if (ModelState.IsValid) {// если все поля верны и прошли про верку то выполняем это условие
                allOrders.createOrder(order);//создание заказа если товары есть в корзине
                return RedirectToAction("Complete");
            }
            return View(order);

        }

        public IActionResult Complete() {
            ViewBag.Message = "Заказ успешно обработан";
            return View();
        }
    }
}
