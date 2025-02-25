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
    public class CommentConsultController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public CommentConsultController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:44388/api/CommentConsult/");
        }

        // GET: api/CommentConsult
        [HttpGet]
        public async Task<IEnumerable<CommentConsult>> Get()
        {
            IEnumerable<CommentConsult> comments = null;

            try
            {
                var response = await _httpClient.GetAsync("GetAllComments");

                if (response.IsSuccessStatusCode)
                {
                    comments = await response.Content.ReadAsAsync<IList<CommentConsult>>();
                }
                else
                {
                    comments = new List<CommentConsult>();
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Error del servidor. Por favor, contacta al administrador.");
            }

            return comments;
        }

        // GET: api/CommentConsult/5
        [HttpGet("{id}")]
        public async Task<CommentConsult> GetById(int id)
        {
            CommentConsult comment = null;

            try
            {
                var response = await _httpClient.GetAsync($"GetCommentById{ id}");

                if (response.IsSuccessStatusCode)
                {
                    comment = await response.Content.ReadAsAsync<CommentConsult>();
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Error del servidor. Por favor, contacta al administrador.");
            }

            return comment;
        }

        // POST: api/CommentConsult
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CommentConsult comment)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("AddComment", comment);

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

        // PUT: api/CommentConsult/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CommentConsult comment)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"UpdateComment/{id}", comment);

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

        // DELETE: api/CommentConsult/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"DeleteComment/{id}");

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
