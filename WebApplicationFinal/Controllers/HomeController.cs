using WebApplicationFinal.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApplicationFinal.ViewModels;

namespace WebApplicationFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAllProduct _carRep;       //переменая для доступа к репозиторию со всеми товарами

        public HomeController(IAllProduct carRep)
        {
            _carRep = carRep;                   //доступ к репозиторию со всеми товарами
        }
        //возвращение шаблона
        public ViewResult Index() {
            var homeCars = new HomeViewModel
            {
                favProduct = _carRep.getFavProduct  //получене популярных машин
            };
            return View(homeCars);
        }
    }
}
