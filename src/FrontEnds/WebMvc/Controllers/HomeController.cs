using MedicalSystem.FrontEnds.WebMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MedicalSystem.FrontEnds.WebMvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult StatusCodePages(int code)
        {
            ViewData["Code"] = code;
            return View();
        }
    }
}
