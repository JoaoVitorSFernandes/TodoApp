namespace TodoApp.Models
{
    public class SubTodo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public DateTime? Date { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Todo Todo { get; set; }
    }
}