using System.ComponentModel.DataAnnotations;

namespace TodoApp.ViewModels
{
    public class EditorTasksViewModels
    {
        [Required]
        [StringLength(60, MinimumLength = 1)]
        public string Title { get; set; }
    }
}