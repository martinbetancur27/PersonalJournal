
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class CreateJournalViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(1200)]
        public string? Message { get; set; }
    }
}
