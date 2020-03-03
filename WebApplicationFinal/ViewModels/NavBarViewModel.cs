using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationFinal.Data.Models;
using WebApplicationFinal.ViewComponents;

namespace WebApplicationFinal.ViewModels
{
    public class NavBarViewModel
    {
        public string SearchValue { get; set; }
        public ShopCart ShopCart { get; set; }

    }
}
