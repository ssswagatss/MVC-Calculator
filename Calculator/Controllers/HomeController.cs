using Calculator.Models;
using System.Web.Mvc;

namespace Calculator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View(new CalculatorVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(CalculatorVM model)
        {
            double res = 0D;
            if (ModelState.IsValid)
            {
                switch (model.CommandText.ToLower())
                {
                    case "add":
                        res = model.FirstNumber + model.SecondNumber;
                        break;

                    case "sub":
                        res = model.FirstNumber - model.SecondNumber;
                        break;

                    case "mul":
                        res = model.FirstNumber * model.SecondNumber;
                        break;

                    case "div":
                        res = model.FirstNumber / model.SecondNumber;
                        break;
                    default:
                        break;
                }
                ViewBag.Result = res;
                return View(model);
            }
            return View(model);
        }
    }
}