using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplicationFinal.Data.Interfaces;
using WebApplicationFinal.Data.Models;
using WebApplicationFinal.ViewModels;

namespace WebApplicationFinal.Controllers
{
    public class DetailController : Controller
    {
        private readonly IAllCars _allCars;
        private readonly ICarsCategory _allCategories;

        public DetailController(IAllCars iAllCars, ICarsCategory iCarsCat)
        {
            _allCars = iAllCars;
            _allCategories = iCarsCat;
        }

        [Route("/detail/index/")]
        public ViewResult Index(int id/*третий параметр в адресной строке*/)
        {
            IEnumerable<Car> cars = null; //сюда помещаем все машины для отображения
            //проверяем пустая ли строка и если она пустая то выводим все машины
            cars = _allCars.Cars.Where(i => i.id.Equals(id));

            var carObj = new DetailViewModel
            {
                getDetailCars = cars,
            };


            return View(carObj);

        }

    }
}
