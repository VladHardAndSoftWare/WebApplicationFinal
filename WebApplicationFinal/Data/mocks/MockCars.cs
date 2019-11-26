using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationFinal.Data.Interfaces;
using WebApplicationFinal.Data.Models;

namespace WebApplicationFinal.Data.mocks
{
    public class MockCars : IAllCars
    {
        private readonly ICarsCategory _categoryCars = new MockCategory(); 
        public IEnumerable<Car> Cars {
            get {
                return new List<Car>
                {
                    new Car {  
                        Name = "Tesla",
                        shortDesc="",
                        longDesc="",
                        img="/img/Tesla.jpg",
                        price=45000,
                        isFavorite=true,
                        availiible=true,
                        Category=_categoryCars.AllCategories.First()
                    },
                     new Car
                     {
                        Name = "Tesla",
                        shortDesc = "",
                        longDesc = "",
                        img = "/img/Tesla.jpg",
                        price = 45000,
                        isFavorite = true,
                        availiible = true,
                        Category = _categoryCars.AllCategories.First()
                     },
                      new Car
                     {
                        Name = "Tesla",
                        shortDesc = "",
                        longDesc = "",
                        img = "/img/Tesla.jpg",
                        price = 45000,
                        isFavorite = true,
                        availiible = true,
                        Category = _categoryCars.AllCategories.First()
                     }
                };
            }
        } 
        public IEnumerable<Car> getFavCars { get; set;}

        public Car getOnbjectCar(int carId)
        {
            throw new NotImplementedException();
        }
    }
}
