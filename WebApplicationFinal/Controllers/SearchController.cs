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
        private readonly IAllProduct _allProduct;
        private readonly IProductCategory _allCategories;

        public SearchController(IAllProduct iAllCars, IProductCategory iCarsCat)
        {
            _allProduct = iAllCars;
            _allCategories = iCarsCat;
        }
        //функция при открытии сайта
        public ActionResult Index()
        {
            IEnumerable<Product> cars = null; //сюда помещаем все машины для отображения
            cars = _allProduct.Product.OrderBy(i => i.id);

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
            IEnumerable<Product> products = null; //сюда помещаем все машины для отображения
            products = _allProduct.Product.Where(i => i.Name.ToLower().Contains(searchName)).OrderBy(i => i.id);//сравниваем введённый текст с БД
            //создаем объект для представления и SearchViewModel
            var carObj = new SearchViewModel
            {
                getAllSearchCars = products
            };

            ViewBag.Title = "Поиск";
            return View(carObj);
        }
    }
}





