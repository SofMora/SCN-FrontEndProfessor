using Microsoft.AspNetCore.Mvc;

namespace SCNProfessor.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
