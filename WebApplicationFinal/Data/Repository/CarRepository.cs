using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebApplicationFinal.Data.Interfaces;
using WebApplicationFinal.Data.Models;

namespace WebApplicationFinal.Data.Repository
{
    public class CarRepository : IAllCars
    {
        private readonly AppDBContent appDBContent;
        //конструктор по умолчанию
        public CarRepository(AppDBContent appDBContent) {
            this.appDBContent = appDBContent;
        }
        public IEnumerable<Car> Cars => appDBContent.Car.Include(c => c.Category);

        public IEnumerable<Car> getFavCars => appDBContent.Car.Where(p => p.isFavorite).Include(c => c.Category);

        public Car getOnbjectCar(int carId) => appDBContent.Car.FirstOrDefault(p => p.id == carId);

    }
}
