using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplicationFinal.Data.Interfaces;
using WebApplicationFinal.Data.Models;
using WebApplicationFinal.ViewModels;

namespace WebApplicationFinal.Controllers
{
    public class ProductController : Controller 
    {
        private readonly IAllProduct _allProduct;
        private readonly IProductCategory _allCategories;

        public ProductController(IAllProduct iAllCars, IProductCategory iCarsCat)
        {
            _allProduct = iAllCars;
            _allCategories = iCarsCat;
        }

        [Route("product/list")]
        [Route("product/list/{category}")]
        public ViewResult List(string category/*третий параметр в адресной строке*/) {

            string _category = category;
            IEnumerable<Product> cars = null; //сюда помещаем все машины для отображения
            string currCategory = "";
            //проверяем пустая ли строка и если она пустая то выводим все машины
            if (string.Equals("all", category, StringComparison.OrdinalIgnoreCase/*не учитвыаем регистр*/))
            {
                cars = _allProduct.Product.OrderBy(i => i.id);
            }
            else
            { //иначе разбиваем на категории
                if (string.Equals("cup", category, StringComparison.OrdinalIgnoreCase/*не учитвыаем регистр*/))//здесь сравниваем строку с "electro"
                {
                    cars = _allProduct.Product.Where(i => i.Category.categoryName.Equals("Кружки")).OrderBy(i => i.id); // здесь записываем все машины из этой категории
                    currCategory = "Кружки";//заполняем заголовок
                }
                else if (string.Equals("glider", category, StringComparison.OrdinalIgnoreCase/*не учитвыаем регистр*/))
                {
                    cars = _allProduct.Product.Where(i => i.Category.categoryName.Equals("Планеры")).OrderBy(i => i.id);
                    currCategory = "Планеры"; //заполняем загловок               
                }
                else if (string.Equals("mask", category, StringComparison.OrdinalIgnoreCase/*не учитвыаем регистр*/))
                {
                    cars = _allProduct.Product.Where(i => i.Category.categoryName.Equals("Маски")).OrderBy(i => i.id);
                    currCategory = "Маски"; //заполняем загловок               
                }
                else if (string.Equals("notebook", category, StringComparison.OrdinalIgnoreCase/*не учитвыаем регистр*/))
                {
                    cars = _allProduct.Product.Where(i => i.Category.categoryName.Equals("Блокноты")).OrderBy(i => i.id);
                    currCategory = "Блокноты"; //заполняем загловок               
                }
            }

            var carObj = new ProductListViewModel
            {
                getAllCars = cars,
                currCategory = currCategory
            };

            ViewBag.Title = "Страница с товарами";
            return View(carObj);
        }
    }
}
