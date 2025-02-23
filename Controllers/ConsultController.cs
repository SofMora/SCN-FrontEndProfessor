using Microsoft.AspNetCore.Mvc;
using SCNProfessor.Models.Domain;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace SCNProfessor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ConsultController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:44388/api/Consult/");
        }

        // GET: api/Consult
        [HttpGet]
        public async Task<IEnumerable<Consult>> Get()
        {
            IEnumerable<Consult> consults = null;

            try
            {
                var response = await _httpClient.GetAsync("GetAllConsults");

                if (response.IsSuccessStatusCode)
                {
                    consults = await response.Content.ReadAsAsync<IList<Consult>>();
                }
                else
                {
                    consults = new List<Consult>();
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Error del servidor. Por favor, contacta al administrador.");
            }

            return consults;
        }

        // GET: api/Consult/5
        [HttpGet("{id}")]
        public async Task<Consult> GetById(int id)
        {
            Consult consult = null;

            try
            {
                var response = await _httpClient.GetAsync($"GetConsultById/{id}");

                if (response.IsSuccessStatusCode)
                {
                    consult = await response.Content.ReadAsAsync<Consult>();
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Error del servidor. Por favor, contacta al administrador.");
            }

            return consult;
        }

        // POST: api/Consult
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Consult consult)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("AddConsult", consult);

                if (response.IsSuccessStatusCode)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch
            {
                return StatusCode(500, "Error del servidor. Por favor, contacta al administrador.");
            }
        }

        // PUT: api/Consult/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Consult consult)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"UpdateConsult/{id}", consult);

                if (response.IsSuccessStatusCode)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch
            {
                return StatusCode(500, "Error del servidor. Por favor, contacta al administrador.");
            }
        }

        // DELETE: api/Consult/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"DeleteConsult/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch
            {
                return StatusCode(500, "Error del servidor. Por favor, contacta al administrador.");
            }
        }
    }
}
