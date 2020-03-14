using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebApplicationFinal.Data.Models;
using WebApplicationFinal.ViewModels;

namespace WebApplicationFinal.ViewComponents
{
    public class NavBarViewComponent: ViewComponent
    {
        private readonly ShopCart _shopCart;
        public NavBarViewComponent(ShopCart shopCart)
        {
            _shopCart = shopCart;
        }

        public ViewViewComponentResult Invoke()
        {
         var items = _shopCart.getShopItems();
            _shopCart.listShopItems = items;
            var NavBarViewModel = new NavBarViewModel
            {
                ShopCart = _shopCart
            };
            return View(NavBarViewModel);
        }
    }
}
