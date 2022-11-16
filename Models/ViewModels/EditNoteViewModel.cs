
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class EditNoteViewModel
    {
        [Required]
        public int IdNote { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        
        [Required]
        [StringLength(15000)]
        public string Message { get; set; }
    }
}
