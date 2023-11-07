using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; } = false;
        public bool Favorite { get; set; } = false;
        public DateTime? Date { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public SubTodo SubTodo { get; set; }

        public int ListTasksId { get; set; }
        public Todos ListTasks { get; set; }

    }
}