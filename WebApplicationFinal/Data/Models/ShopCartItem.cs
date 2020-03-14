namespace WebApplicationFinal.Data.Models
{
    public class ShopCartItem
    {
        public int id { get; set; }
        public Product car { get; set; }
        public int productId { get; set; }
        public int price { get; set; }
        public string ShopCartId { get; set; }
        public int Quantity { get; set; }
    }
}
