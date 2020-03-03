using System.Collections.Generic;
using WebApplicationFinal.Data.Models;

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationFinal.ViewModels
{
    public class OrderViewModel
    {
        public Order Order { get; set; }
        public List<ShopCartItem> listShopItems { get; set; }

    }
}
