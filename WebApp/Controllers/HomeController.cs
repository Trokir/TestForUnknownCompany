
using System.Web.Mvc;
using WebApp.BL;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private IProcessCalculation _calculation;

        public HomeController()
        {
            _calculation = new ProcessCalculation();
        }



        public ActionResult Index()
        {

            return View();
        }
       [HttpPost]
        public ActionResult CalcResult(Calculator calc)
        {
            calc.Result = _calculation.GetResult(calc.Factor);
           
            if (!ModelState.IsValid)
            {
                return HttpNotFound();

            }
            return PartialView(calc);
        }
    }
}