using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.Models
{
    public sealed class Todos
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Todo> Tasks { get; set; } = new();
    }
}