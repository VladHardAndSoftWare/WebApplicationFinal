using System.Collections.Generic;
using WebApplicationFinal.Data.Models;

namespace WebApplicationFinal.Data.Interfaces
{
     public interface ICarsCategory
    {
        IEnumerable<Category> AllCategories { get; }
    }
}
