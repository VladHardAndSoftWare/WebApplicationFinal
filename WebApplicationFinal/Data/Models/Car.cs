namespace WebApplicationFinal.Data.Models
{
    public class Car
    {
        public int id { set; get; }
        public string Name { set; get; }
        public string shortDesc { set; get; }
        public string longDesc { set; get; }
        public string img { set; get; }
        public ushort price { set; get; }
        public bool isFavorite { set; get; }
        public bool availiible { set; get; }
        public int categoryID { set; get; }
        public virtual Category Category { set; get; }
    }
}
