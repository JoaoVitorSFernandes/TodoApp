namespace TodoApp.Models
{
    public sealed class Task
    {
        public int Order { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Task? SubTask { get; set; }
    }
}