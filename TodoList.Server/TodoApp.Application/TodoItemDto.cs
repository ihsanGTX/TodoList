namespace TodoApp.Application.DTOs
{
    public class TodoItemDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public bool IsCompleted { get; set; }
    }
}
