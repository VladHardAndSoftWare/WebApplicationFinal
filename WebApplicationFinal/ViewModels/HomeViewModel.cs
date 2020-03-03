using System.Collections.Generic;
using WebApplicationFinal.Data.Models;

namespace WebApplicationFinal.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> favProduct { get; set; }

    }
}
