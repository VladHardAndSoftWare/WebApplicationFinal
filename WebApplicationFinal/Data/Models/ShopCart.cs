using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplicationFinal.Data.Models
{
    public class ShopCart
    {
        private readonly AppDBContent appDBContent;
        //конструктор по умолчанию
        public ShopCart(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }
        public string ShopCartId { get; set; }

        public List<ShopCartItem> listShopItems { get; set; }
        //функция проверяет существует ли корзина или нет
        public static ShopCart GetCart(IServiceProvider services) {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = services.GetService<AppDBContent>();
            string shopCartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId" , shopCartId);

            return new ShopCart(context) { ShopCartId = shopCartId };
        }
        //функия отвечает за добавление товаров в корзину
        public void AddToCart(Product car) {
            appDBContent.ShopCartItem.Add(new ShopCartItem
            {
                ShopCartId = ShopCartId,
                car = car,
                price = car.price

            });

            appDBContent.SaveChanges();
        }

        public void RemoveFromCart(int id)
        {
            Console.WriteLine("id: " + id);
            ShopCartItem obj = appDBContent.ShopCartItem.Find(id);
            Console.WriteLine("obj: "+ obj);
            appDBContent.ShopCartItem.Remove(obj);

            appDBContent.SaveChanges();
        }

        //функция отвечающая за отображение товаров в корзине
        public List<ShopCartItem> getShopItems() {
            return appDBContent.ShopCartItem.Where(c => c.ShopCartId == ShopCartId).Include(s => s.car).ToList();
        }

    }
}
