using System.ComponentModel.DataAnnotations;

namespace TodoApp.ViewModels
{
    public sealed class EditorTaskViewModel
    {
        [Required]
        [StringLength(35, MinimumLength = 3)]
        public string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? Date { get; set; }

        [Required]
        public int ListOrTasksId { get; set; }
    }
}