using System.Collections.Generic;
using WebApplicationFinal.Data.Models;

namespace WebApplicationFinal.Data.Interfaces
{
    public interface IAllCars
    {
        IEnumerable<Car> Cars { get; }
        IEnumerable<Car> getFavCars { get; }
        Car getOnbjectCar(int carId);

    }
}
