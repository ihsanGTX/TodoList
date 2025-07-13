using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Services;
using TodoApp.Core.Entities;

namespace TodoApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoService _service;

        public TodoController(TodoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var todos = await _service.GetTodosAsync();
            return Ok(todos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var todo = await _service.GetTodoByIdAsync(id);
            if (todo == null) return NotFound();
            return Ok(todo);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TodoItem todo)
        {
            try
            {
                await _service.AddTodoAsync(todo.Content);
                return CreatedAtAction(nameof(GetById), new { id = todo.Id }, todo);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TodoItem todo)
        {
            if (id != todo.Id) return BadRequest("ID mismatch");

            if (string.IsNullOrWhiteSpace(todo.Content))
                return BadRequest("Content cannot be empty");

            try
            {
                await _service.UpdateTodoAsync(todo);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteTodoAsync(id);
            return NoContent();
        }
    }
}
