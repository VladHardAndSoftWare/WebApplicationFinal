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
        private readonly IAllProduct _allCars;
        private readonly IProductCategory _allCategories;

        public SearchController(IAllProduct iAllCars, IProductCategory iCarsCat)
        {
            _allCars = iAllCars;
            _allCategories = iCarsCat;
        }
        //функция при открытии сайта
        public ActionResult Index()
        {
            IEnumerable<Product> cars = null; //сюда помещаем все машины для отображения
            cars = _allCars.Product.OrderBy(i => i.id);

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
            string searchName = search.SearchValue;
            if (search.SearchValue != null)
            {
                searchName = search.SearchValue.ToLower();
            }
            IEnumerable<Product> cars = null; //сюда помещаем все машины для отображения
            cars = _allCars.Product.Where(i => i.Name.ToLower().Contains(searchName)).OrderBy(i => i.id);//сравниваем введённый текст с БД
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





