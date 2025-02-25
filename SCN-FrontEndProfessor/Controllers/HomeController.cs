using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace LabSandboxC15034.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        // StudentDAO studentDAO;
        //NationalityDAO nationalityDAO; 


        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
            //To Do Instance student DAO
            //StudentDAO studentDAO = new StudentDAO(_configuration);
        }

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult GetAllStudents()
        //{
        //    studentDAO = new StudentDAO(_configuration);
        //    return Ok(studentDAO.GetAll());
        //}
        //public IActionResult GetStudentByEmail(string email)
        //{
        //    studentDAO = new StudentDAO(_configuration);
        //    return Ok(studentDAO.Get(email));
        //}

        //public IActionResult Insert([FromBody] Student student)
        //{
        //    try
        //    {
        //        studentDAO = new StudentDAO(_configuration);

        //        if (studentDAO.Get(student.Email).Email == null)
        //        {
        //            int result = studentDAO.Insert(student);
        //            return Ok(result);
        //        }
        //        else
        //        {
        //            return Error();
        //        }
        //    }
        //    catch (SqlException e)
        //    {
        //        //TODO send the most maninful message to the front-end the user to see 

        //        return Error();
        //        // return View(e.ToString()); Idea para localizar los errores 
        //        //ViewBag.Message = e.Message;
        //    }


        //}

        //public IActionResult DeleteStudent(string email)
        //{
        //    try
        //    {
        //        studentDAO = new StudentDAO(_configuration);

        //        return Ok(studentDAO.Delete(email));
        //    }
        //    catch (SqlException e)
        //    {
        //        return Error();

        //    }



        //}

        ////public IActionResult GetNationalities()
        ////{
        ////    nationalityDAO = new NationalityDAO(_configuration);

        ////    return Json(nationalityDAO.Get());

        ////}


        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

        //public IActionResult UpdateStudent([FromBody] Student student)
        //{
        //    //TODO: handle exception appropriately and send meaningful message to the view
        //    studentDAO = new StudentDAO(_configuration);
        //    return Ok(studentDAO.Update(student));

        //}





    }


}
