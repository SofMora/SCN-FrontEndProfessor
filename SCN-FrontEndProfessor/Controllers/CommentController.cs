using Microsoft.AspNetCore.Mvc;
using SCNProfessor.Models.Domain;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SCNProfessor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public CommentController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:44388/api/Comment/");
        }

        // GET: api/Comment
        [HttpGet]
        public async Task<IEnumerable<Comment>> Get()
        {
            var comments = await _httpClient.GetFromJsonAsync<IEnumerable<Comment>>("GetAllComments");
            return comments ?? new List<Comment>();
        }

        // GET: api/Comment/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetById(int id)
        {
            var comment = await _httpClient.GetFromJsonAsync<Comment>($"GetCommentById/{id}");
            if (comment == null)
            {
                return NotFound();
            }
            return comment;
        }

        [HttpGet("news/{newsId}/comments")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByNewsId(int newsId)
        {
            // Lógica para obtener los comentarios asociados a la noticia con el ID proporcionado
            var comments = await _httpClient.GetFromJsonAsync<IEnumerable<Comment>>($"GetCommentsByNewsId/{newsId}");
            if (comments == null || !comments.Any())
            {
                return NotFound("No se encontraron comentarios para esta noticia.");
            }
            return Ok(comments);
        }


        // POST: api/Comment
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Comment comment)
        {
            var response = await _httpClient.PostAsJsonAsync("AddComment", comment);
            if (response.IsSuccessStatusCode)
            {
                var createdComment = await response.Content.ReadFromJsonAsync<Comment>();
                return CreatedAtAction(nameof(GetById), new { id = createdComment.Id }, createdComment);
            }
            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }

        // PUT: api/Comment/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            var response = await _httpClient.PutAsJsonAsync($"UpdateComment/{id}", comment);
            if (response.IsSuccessStatusCode)
            {
                return NoContent();
            }
            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }

        // DELETE: api/Comment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"DeleteComment/{id}");
            if (response.IsSuccessStatusCode)
            {
                return NoContent();
            }
            return StatusCode((int)response.StatusCode, response.ReasonPhrase);
        }
    }
}
