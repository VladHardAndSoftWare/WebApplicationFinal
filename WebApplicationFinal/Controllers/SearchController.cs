using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplicationFinal.Data.Interfaces;
using WebApplicationFinal.Data.Models;
using WebApplicationFinal.ViewModels;

namespace WebApplicationFinal.Controllers
{
    public class SearchController : Controller
    {
        private readonly IAllCars _allCars;
        private readonly ICarsCategory _allCategories;

        public SearchController(IAllCars iAllCars, ICarsCategory iCarsCat)
        {
            _allCars = iAllCars;
            _allCategories = iCarsCat;
        }
        //функция при открытии сайта
        public ActionResult Index()
        {
            IEnumerable<Car> cars = null; //сюда помещаем все машины для отображения
            cars = _allCars.Cars.OrderBy(i => i.id);

            var carObj = new SearchViewModel
            {
                getAllSearchCars = cars
            };

            ViewBag.Title = "Поиск";
            return View(carObj);
        }
        //функция выполняемая при поиске товаров
        [HttpPost]
        public ActionResult Index(SearchViewModel search)
        {
            string searchName = "";
            if (search.SearchValue != null)
            {
                searchName = search.SearchValue.ToLower();
            }
            IEnumerable<Car> cars = null; //сюда помещаем все машины для отображения
            cars = _allCars.Cars.Where(i => i.Name.ToLower().Contains(searchName)).OrderBy(i => i.id);//сравниваем введённый текст с БД
            //создаем объект для представления и SearchViewModel
            var carObj = new SearchViewModel
            {
                getAllSearchCars = cars
            };

            ViewBag.Title = "Поиск";
            return View(carObj);
        }
    }
}





