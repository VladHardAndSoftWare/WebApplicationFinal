using System.Collections.Generic;

namespace WebApplicationFinal.Data.Models
{
    public class Category
    {
        public int id { set; get; }
        public string categoryName { set; get; }
        public string desc { set; get; }
        public List<Product> cars{ set; get; }

    }
}
