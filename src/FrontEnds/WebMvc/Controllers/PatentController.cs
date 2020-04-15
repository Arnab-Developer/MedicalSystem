using Microsoft.AspNetCore.Mvc;

namespace MedicalSystem.FrontEnds.WebMvc.Controllers
{
    public class PatentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
