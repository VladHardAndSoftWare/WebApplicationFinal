using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationFinal.Data.Models;

namespace WebApplicationFinal.ViewModels
{
    public class DetailViewModel
    {
        public IEnumerable<Car> getDetailCars { get; set; }
        public Car Car { get; set; }
    }
}
