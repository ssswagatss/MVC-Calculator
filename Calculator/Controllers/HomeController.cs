using Calculator.Helpers;
using Calculator.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Calculator.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.LastResult = GetLastResults();
            return View();
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
                SetLastResult(model, res);
                ViewBag.LastResult = GetLastResults();
                return View(model);
            }
            ViewBag.LastResult = GetLastResults();
            return View(model);
        }
        private List<ResultVM> GetLastResults()
        {
            return Session["LastResults"] == null ? new List<ResultVM>() : (List<ResultVM>)Session["LastResults"];
        }

        private void SetLastResult(CalculatorVM model, double result)
        {
            var lastResults = Session["LastResults"] == null ? new List<ResultVM>() : (List<ResultVM>)Session["LastResults"];
            lastResults.Insert(0, new ResultVM
            {
                FirstNumber = model.FirstNumber,
                SecondNumber = model.SecondNumber,
                CommandText = model.CommandText,
                Result = result,
                CommandOperator = StringHelper.GetOperator(model.CommandText)
            });

            if (lastResults.Count > 3)
                Session["LastResults"] = lastResults.Take(3).ToList();
            else
                Session["LastResults"] = lastResults;
        }
    }
}