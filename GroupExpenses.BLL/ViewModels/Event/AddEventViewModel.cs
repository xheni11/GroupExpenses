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

        // TODO integration with loaction extrenal api
        public string? Location { get; set; }
    }
}
