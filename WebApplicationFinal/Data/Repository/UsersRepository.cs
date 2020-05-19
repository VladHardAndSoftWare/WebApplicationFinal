using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplicationFinal.Data.Models;
using WebApplicationFinal.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebApplicationFinal.Data.Repository
{
    public class UsersRepository : IAllUsers
    {
        private readonly AppDBContent appDBContent;
        public UsersRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }
        public IEnumerable<Users> Users => appDBContent.Users.Include(c => c.login);

       // public Product getOnbjectProduct(int carId) => appDBContent.Users.FirstOrDefault(p => p.id == UserId);

    }
}
