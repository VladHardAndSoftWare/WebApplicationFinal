using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationFinal.Data.Interfaces;
using WebApplicationFinal.Data.Models;

namespace WebApplicationFinal.Data.mocks
{
    public class MockCars : IAllProduct
    {
        private readonly IProductCategory _categoryCars = new MockCategory(); 
        public IEnumerable<Product> Product {
            get {
                return new List<Product>
                {
                    new Product {  
                        Name = "Tesla",
                        shortDesc="",
                        longDesc="",
                        img="/img/Tesla.jpg",
                        price=45000,
                        isFavorite=true,
                        availiible=true,
                        Category=_categoryCars.AllCategories.First()
                    },
                     new Product
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
                      new Product
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
        public IEnumerable<Product> getFavProduct { get; set;}

        public Product getOnbjectProduct(int carId)
        {
            throw new NotImplementedException();
        }
    }
}
