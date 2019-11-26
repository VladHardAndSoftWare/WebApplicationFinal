using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationFinal.Data.Interfaces;
using WebApplicationFinal.Data.Models;

namespace WebApplicationFinal.Data.mocks
{
    public class MockCategory : ICarsCategory
    {
        public IEnumerable<Category> AllCategories
        {
            get
            {
                return new List<Category>
                {
                    new Category { categoryName =  "Электромобили", desc="Современный вид транспорта"},
                    new Category { categoryName = "Классические автомобили", desc="Машины с двигателем внутреннего сгорания" }
                }; 
           }
        }
        
    }
}
