using GAB.DistanceCalculation.Presentation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GAB.DistanceCalculation.Presentation.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
