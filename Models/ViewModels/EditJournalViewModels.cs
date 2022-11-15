
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class EditJournalViewModels
    {
        [Required]
        public int IdJournal { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(1200)]
        public string? Message { get; set; }
    }
}
