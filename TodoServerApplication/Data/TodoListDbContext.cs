using Microsoft.EntityFrameworkCore;
using TodoServerApplication.Models;

namespace TodoServerApplication.Data
{
    public class TodoListDbContext : DbContext
    {
        public TodoListDbContext(DbContextOptions options) : base(options) { }

        public DbSet<TodoItem> Todos { get; set; }

        
    }
}
