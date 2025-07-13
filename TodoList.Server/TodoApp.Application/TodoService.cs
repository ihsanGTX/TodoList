using TodoApp.Core.Entities;
using TodoApp.Core.Interfaces;

namespace TodoApp.Application.Services
{
    public class TodoService
    {
        private readonly ITodoRepository _repository;

        public TodoService(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TodoItem>> GetTodosAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<TodoItem?> GetTodoByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddTodoAsync(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentException("Content cannot be empty");

            var todo = new TodoItem { Content = content };
            await _repository.AddAsync(todo);
        }

        public async Task UpdateTodoAsync(TodoItem item)
        {
            if (string.IsNullOrWhiteSpace(item.Content))
                throw new ArgumentException("Content cannot be empty");

            await _repository.UpdateAsync(item);
        }

        public async Task DeleteTodoAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
