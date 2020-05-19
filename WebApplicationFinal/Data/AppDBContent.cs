using Microsoft.EntityFrameworkCore;
using WebApplicationFinal.Data.Models;

namespace WebApplicationFinal.Data
{
    public class AppDBContent : DbContext//регистрация таблиц баз данных
    {
        //констркутор по умолчанию
        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options) { 
        
        }
        //Получение и установка всех товаров в магазине из базы данных
        public DbSet<Product> Product { get; set; }
        //Получение и установка всех категорий в магазине из базы данных
        public DbSet<Category> Category { get; set; }
        public DbSet<ShopCartItem> ShopCartItem { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Users> Users { get; set; }

    }
}
