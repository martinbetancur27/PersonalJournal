
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class CreateJournalViewModel
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        
        [StringLength(1200)]
        public string? Message { get; set; }
    }
}
