using System;
using WebApplicationFinal.Data.Interfaces;
using WebApplicationFinal.Data.Models;

namespace WebApplicationFinal.Data.Repository
{
    public class OrdersRepository : IAllOrder 
    {
        private readonly AppDBContent appDBContent;
        private readonly ShopCart shopCart;

        public OrdersRepository(AppDBContent appDBContent, ShopCart shopCart) {
            this.appDBContent = appDBContent;
            this.shopCart = shopCart;
        }
        public void createOrder(Order order)
        {
            order.orderTime = DateTime.Now;
            appDBContent.Order.Add(order);

            var items = shopCart.listShopItems;

            foreach (var el in items) {
                var orderDetail = new OrderDetail()
                {
                    CarID = el.car.id,
                    orderID = order.id,
                    price = el.car.price
                };
                appDBContent.OrderDetail.Add(orderDetail);
            }
            appDBContent.SaveChanges();
           // throw new NotImplementedException();
        }
    }
}
