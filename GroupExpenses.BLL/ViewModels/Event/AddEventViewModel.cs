using System.ComponentModel.DataAnnotations;

namespace GroupExpenses.BLL.ViewModels.Event
{
    public record AddEventViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string? Details { get; set; }
        public string? Location { get; set; }
    }
}
