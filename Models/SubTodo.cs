namespace TodoApp.Models
{
    public class SubTodo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; }
        public bool Favorite { get; set; } = false;
        public DateTime? Date { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int TodoId { get; set; }
        public Todo Todo { get; set; }
    }
}