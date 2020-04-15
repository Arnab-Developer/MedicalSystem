using Microsoft.AspNetCore.Mvc;

namespace MedicalSystem.FrontEnds.WebMvc.Controllers
{
    public class ConsultationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
