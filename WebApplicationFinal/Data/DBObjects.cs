using System.Collections.Generic;
using System.Linq;
using WebApplicationFinal.Data.Models;

namespace WebApplicationFinal.Data
{
    public class DBObjects
    {
        public static void Initial(AppDBContent content)
        {
            if (!content.Category.Any())
                content.Category.AddRange(Categories.Select(c => c.Value));

            if (!content.Car.Any())
            {
                content.AddRange(
                    new Product
                    {
                        Name = "Кружка 'В Питере - пить'",
                        shortDesc = "",
                        longDesc = "",
                        img = "https://sun9-22.userapi.com/c851532/v851532593/d9bca/JPWnY8O51U4.jpg",
                        price = 400,
                        isFavorite = true,
                        availiible = true,
                        Category = Categories["Электромобили"]
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
                         Category = Categories["Электромобили"]
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
                          Category = Categories["Электромобили"]
                      }
                );
                // сохренение изменений в базе данных
                content.SaveChanges();
            }
        }


        private static Dictionary<string, Category> category;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (category == null)
                {
                    var list = new Category[] {
                        new Category { categoryName = "Электромобили", desc = "Современный вид транспорта" },
                        new Category { categoryName = "Классические автомобили", desc = "Машины с двигателем внутреннего сгорания" }
                    };

                    category = new Dictionary<string, Category>();
                    foreach (Category el in list)
                        category.Add(el.categoryName, el);
                }

                return category;
            }
        }
    }
}
