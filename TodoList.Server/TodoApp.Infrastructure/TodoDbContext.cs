using Microsoft.EntityFrameworkCore;
using TodoApp.Core.Entities;

namespace TodoApp.Infrastructure.Data
{
    public class TodoDbContext : DbContext
    {
        public DbSet<TodoItem> TodoItems { get; set; }

        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }
    }
}
