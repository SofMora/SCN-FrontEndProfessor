using Microsoft.AspNetCore.Mvc;
using SCNProfessor.Models.Domain;


namespace SCNProfessor.Controllers
{
    public class ProfessorController : ControllerBase
    {
        // GET: api/<ProfessorController>
        [HttpGet]
        public IEnumerable<Professor> Get()
        {
            IEnumerable<Professor> professors = null;

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7250/api/Professor/");
                    var responseTask = client.GetAsync("GetAllProfessors");
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<IList<Professor>>();
                        readTask.Wait();
                        // Lee los profesores provenientes de la API
                        professors = readTask.Result;
                    }
                    else
                    {
                        professors = Enumerable.Empty<Professor>();
                    }
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Server error. Please contact an administrator");
            }

            return professors;
        }

        [HttpGet]
        public Professor GetById(int id)
        {
            Professor professor = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7250/api/Professor/GetProfessorById/" + id);
                var responseTask = client.GetAsync(client.BaseAddress);
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Professor>();
                    readTask.Wait();
                    // Lee el profesor proveniente de la API
                    professor = readTask.Result;
                }
            }

            return professor;
        }

        // POST api/<ProfessorController>
        [HttpPost]
        public IActionResult Post([FromBody] Professor professor)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7250/api/Professor/");

                var postTask = client.PostAsJsonAsync("AddProfessor", professor);
                postTask.Wait();

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return new JsonResult(result);
                    // TODO: return new JsonResult(professor);
                }
                else
                {
                    // TODO should be customized to meet the client's needs
                    return new JsonResult(result);
                }
            }
        }

        // PUT api/<ProfessorController>/5
        [HttpPut]
        public IActionResult Put([FromBody] Professor professor, int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44388/api/Professor/UpdateProfessor/" + id);
                var putTask = client.PutAsJsonAsync("UpdateProfessor/" + professor.Id, professor);
                putTask.Wait();

                var result = putTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return new JsonResult(result);
                    // TODO: return new JsonResult(professor);
                }
                else
                {
                    // TODO should be customized to meet the client's needs
                    return new JsonResult(result);
                }
            }
        }

        // DELETE api/<ProfessorController>/5
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7250/api/Professor/");

                // HTTP DELETE
                var deleteTask = client.DeleteAsync("DeleteProfessor/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return new JsonResult(result);
                }
                else
                {
                    // Camino del error
                    return new JsonResult(result);
                }
            }
        }
    }
}