namespace WebApplicationFinal.Data.Models
{
    public class OrderDetail
    {
        public int id { get; set; }
        public int orderID { get; set; }
        public int CarID { get; set; }
        public uint price { get; set; }
        public virtual Product car { get; set; }
        public virtual Order order { get; set; }
    }
}
