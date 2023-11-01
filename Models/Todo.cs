using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public DateTime? Date { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public IList<SubTodo> SubTodo { get; set; }

        public Todos ListTasks { get; set; }
    }
}