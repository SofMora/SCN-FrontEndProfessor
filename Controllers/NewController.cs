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
    public class NewController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public NewController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:44388/api/News/");
        }

        // GET: api/News
        [HttpGet]
        public async Task<IEnumerable<New>> Get()
        {
            IEnumerable<New> newsList = null;

            try
            {
                var response = await _httpClient.GetAsync("GetAllNews");

                if (response.IsSuccessStatusCode)
                {
                    newsList = await response.Content.ReadAsAsync<IList<New>>();
                }
                else
                {
                    newsList = new List<New>();
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Error del servidor. Por favor, contacta al administrador.");
            }

            return newsList;
        }

        // GET: api/News/5
        [HttpGet("{id}")]
        public async Task<New> GetById(int id)
        {
            New news = null;

            try
            {
                var response = await _httpClient.GetAsync($"GetNewsById/{id}");

                if (response.IsSuccessStatusCode)
                {
                    news = await response.Content.ReadAsAsync<New>();
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Error del servidor. Por favor, contacta al administrador.");
            }

            return news;
        }

        // POST: api/News
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] New news)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("AddNews", news);

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

        // PUT: api/News/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] New news)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"UpdateNews/{id}", news);

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

        // DELETE: api/News/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"DeleteNews/{id}");

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
