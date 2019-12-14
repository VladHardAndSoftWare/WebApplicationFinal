using System.Collections.Generic;
using WebApplicationFinal.Data.Models;

namespace WebApplicationFinal.Data.Interfaces
{
     public interface IProductCategory
    {
        IEnumerable<Category> AllCategories { get; }
    }
}
