using System.Collections.Generic;
using WebApplicationFinal.Data.Models;

namespace WebApplicationFinal.ViewModels
{
    public class HomeViewModel
    {
        //internal IEnumerable<Car> favCars;

        public IEnumerable<Product> favProduct { get; set; }
        public SearchViewModel SearchViewModel { get; set; }
        public ShopCartViewModel ShopCartViewModel { get; set; }
        public ShopCart ShopCart { get; set; }
    }
}
