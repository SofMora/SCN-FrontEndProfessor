using Microsoft.AspNetCore.Mvc;
using SCNProfessor.Models.Domain;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SCNProfessor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        // GET: api/Course
        [HttpGet]
        public async Task<IEnumerable<Course>> Get()
        {
            IEnumerable<Course> courses = null;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7250/api/Course/");
                    var responseTask =  client.GetAsync("GetAllCourses");
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<Course>>();
                        readTask.Wait();
                        // Lee los cursos provenientes de la API
                        courses = readTask.Result;
                    }
                    else
                    {
                        courses = new List<Course>();
                    }
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact an administrator");
            }

            return courses;
        }

        // GET: api/Course/5
        [HttpGet("{id}")]
        public async Task<Course> GetById(int id)
        {
            Course course = null;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri($"https://localhost:7250/api/Course/GetCourseById/{id}");
                    var responseTask =  client.GetAsync(client.BaseAddress);
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<Course>();
                        readTask.Wait();
                        // Lee el curso proveniente de la API
                        course = readTask.Result;
                    }
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact an administrator");
            }

            return course;
        }

        // POST: api/Course
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Course course)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7250/api/Course/");

                var postTask =  client.PostAsJsonAsync("AddCourse", course);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return new JsonResult(result);
                }
                else
                {
                    // TODO should be customized to meet the client's needs
                    return new JsonResult(result);
                }
            }
        }

        // PUT: api/Course/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Course course)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"https://localhost:7250/api/Course/");

                var putTask =  client.PutAsJsonAsync($"UpdateCourse/{course.Id}", course);
                putTask.Wait();

                var result = putTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return new JsonResult(result);
                }
                else
                {
                    // TODO should be customized to meet the client's needs
                    return new JsonResult(result);
                }
            }
        }

        // DELETE: api/Course/5
        [HttpDelete("{id}")]
        public async Task<JsonResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7250/api/Course/");

                // HTTP DELETE
                var deleteTask =  client.DeleteAsync($"DeleteCourse/{id}");
                deleteTask.Wait();

                var result = deleteTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return new JsonResult(result);
                }
                else
                {
                    return new JsonResult(result);
                }
            }
        }
    }
}
