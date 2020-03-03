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
            ViewBag.Title = "Лучшие товары - Енот и Панда";
           
            var homeCars = new HomeViewModel
            {
                favProduct = _carRep.getFavProduct  //получене популярных машин
            };
            return View(homeCars);
        }


        //[System.Web.Mvc.ChildActionOnly]
        //public ActionResult NavBar(string id)
        //{

        //     return PartialView("NavBar", SearchViewModel);
        //}

        //public ActionResult Partial()
        //{
        //    var obj = new ShopCartViewModel
        //    {

        //    };
        //    return PartialView(obj);
        //}

        public ActionResult NavBar()
        {
            return PartialView("NavBar");
        }
    }
}
