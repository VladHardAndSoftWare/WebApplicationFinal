using System.Collections.Generic;
using System.Linq;
using WebApplicationFinal.Data.Models;

namespace WebApplicationFinal.Data
{
    public class DBObjects
    {

        public static void Initial(AppDBContent content)
        {
          // if (!content.Category.Any())
          //      content.Category.AddRange(Categories.Select(c => c.Value));

          //  if (!content.Product.Any())
          //  {
          //  }
        }


        //private static Dictionary<string, Category> category;
        /*public static Dictionary<string, Category> Categories
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
        }*/
    }
}
