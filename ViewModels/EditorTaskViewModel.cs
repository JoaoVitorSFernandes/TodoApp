using System.ComponentModel.DataAnnotations;

namespace TodoApp.ViewModels
{
    public sealed class EditorTaskViewModel
    {
        public int ListTasksId { get; set; }

        [Required]
        [StringLength(35, MinimumLength = 3)]
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }
    }
}