using Microsoft.AspNetCore.Mvc;
using SocialApp.Services;
using System.Threading.Tasks;
using WebApplicationFinal.Data.Interfaces;
using WebApplicationFinal.Data.Models;
using WebApplicationFinal.ViewModels;

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
            ViewBag.Title = "Оформление заказа - Енот и Панда";
            var orderView = new OrderViewModel
            {

            };
            return View(orderView);
        }

        [HttpPost]//принимает post запрос от Checkout.cshtml
        public IActionResult Checkout(Order order) {
            
            shopCart.listShopItems = shopCart.getShopItems();//берём все товары из корзины и встраиваем их в список
            var orderView = new OrderViewModel
            {
            };
            if (shopCart.listShopItems.Count == 0) {//выполняем если ничего нет в корзине
                    ModelState.AddModelError("", "У вас должны быть товары!");//выдача ошибки при отутсттвии товаров в корзине
                }
                if (ModelState.IsValid) {// если все поля верны и прошли про верку то выполняем это условие
                    allOrders.createOrder(order);//создание заказа если товары есть в корзине
                    _ = SendMessage(order);
                    return RedirectToAction("Complete");
                }
            return View(orderView);

        }
        public IActionResult Complete() {    
            ViewBag.Message = "Заказ успешно обработан";
            return View();
        }

        public async Task<IActionResult> SendMessage(Order order)
        {
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(order.email, "Ваш заказ ожидает оплаты", order.orderTime, shopCart.listShopItems);
            //await emailService.SendEmailAsync("enotwithpanda@yandex.ru", email, shopCart.listShopItems.ToString(), shopCart.listShopItems);
            
            return RedirectToAction("Index");
        }
    }
}
