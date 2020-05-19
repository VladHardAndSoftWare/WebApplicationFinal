using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationFinal.Data.Models
{
    public class Users
    {
        public int id { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string adress { get; set; }
        public int phone { get; set; }
        public string email { get; set; }
    }
}
