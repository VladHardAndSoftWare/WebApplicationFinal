using WebApplicationFinal.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WebApplicationFinal.ViewModels;

namespace WebApplicationFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAllCars _carRep;       //переменая для доступа к репозиторию со всеми товарами

        public HomeController(IAllCars carRep)
        {
            _carRep = carRep;                   //доступ к репозиторию со всеми товарами
        }
        //возвращение шаблона
        public ViewResult Index() {
            var homeCars = new HomeViewModel
            {
                favCars = _carRep.getFavCars  //получене популярных машин
            };
            return View(homeCars);
        }
    }
}
