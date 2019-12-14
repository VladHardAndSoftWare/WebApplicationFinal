using System.Collections.Generic;
using WebApplicationFinal.Data.Models;

namespace WebApplicationFinal.ViewModels
{
    public class CarListViewModel
    {
        public IEnumerable<Product> getAllCars { get; set; }

        public string currCategory { get; set; }
    }
}
