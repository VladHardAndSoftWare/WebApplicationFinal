using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApplicationFinal.Data.Interfaces;
using WebApplicationFinal.Data.Models;

namespace WebApplicationFinal.Data.Repository
{
    public class ProductRepository : IAllProduct
    {
        private readonly AppDBContent appDBContent;
        //конструктор по умолчанию
        public ProductRepository(AppDBContent appDBContent) {
            this.appDBContent = appDBContent;
        }
        public IEnumerable<Product> Product => appDBContent.Product.Include(c => c.Category);

        public IEnumerable<Product> getFavProduct => appDBContent.Product.Where(p => p.isFavorite).Include(c => c.Category);

        public Product getOnbjectProduct(int carId) => appDBContent.Product.FirstOrDefault(p => p.id == carId);

    }
}
