
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class EditJournalViewModels
    {
        [Required]
        public int IdJournal { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(1200)]
        public string? Message { get; set; }
    }
}
