using System.Collections.Generic;
using WebApplicationFinal.Data.Models;

namespace WebApplicationFinal.Data.Interfaces
{
    public interface IAllProduct
    {
        IEnumerable<Product> Product { get; }
        IEnumerable<Product> getFavProduct { get; }
        Product getOnbjectProduct(int carId);

    }
}
