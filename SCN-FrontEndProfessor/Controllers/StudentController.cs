using Microsoft.AspNetCore.Mvc;

namespace SCNProfessor.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
