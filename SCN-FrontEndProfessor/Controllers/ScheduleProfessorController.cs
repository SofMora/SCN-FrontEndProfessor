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
    public class ScheduleController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public ScheduleController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:44388/api/ScheduleProfessor/");
        }

        // GET: api/Schedule
        [HttpGet]
        public async Task<IEnumerable<ScheduleProfessor>> Get()
        {
            IEnumerable<ScheduleProfessor> schedules = null;

            try
            {
                var response = await _httpClient.GetAsync("GetAllSchedules");

                if (response.IsSuccessStatusCode)
                {
                    schedules = await response.Content.ReadAsAsync<IList<ScheduleProfessor>>();
                }
                else
                {
                    schedules = new List<ScheduleProfessor>();
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Error del servidor. Por favor, contacta al administrador.");
            }

            return schedules;
        }

        // GET: api/Schedule/5
        [HttpGet("{id}")]
        public async Task<ScheduleProfessor> GetById(int id)
        {
            ScheduleProfessor schedule = null;

            try
            {
                var response = await _httpClient.GetAsync($"GetScheduleById/{id}");

                if (response.IsSuccessStatusCode)
                {
                    schedule = await response.Content.ReadAsAsync<ScheduleProfessor>();
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Error del servidor. Por favor, contacta al administrador.");
            }

            return schedule;
        }

        // POST: api/Schedule
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ScheduleProfessor schedule)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("AddSchedule", schedule);

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

        // PUT: api/Schedule/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ScheduleProfessor schedule)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"UpdateSchedule/{id}", schedule);

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

        // DELETE: api/Schedule/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"DeleteSchedule/{id}");

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
