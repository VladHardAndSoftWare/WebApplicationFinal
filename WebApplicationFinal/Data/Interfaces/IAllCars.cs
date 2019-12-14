using System.Collections.Generic;
using WebApplicationFinal.Data.Models;

namespace WebApplicationFinal.Data.Interfaces
{
    public interface IAllCars
    {
        IEnumerable<Product> Cars { get; }
        IEnumerable<Product> getFavCars { get; }
        Product getOnbjectCar(int carId);

    }
}
