
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class EditNoteViewModel
    {
        [Required]
        public int IdNote { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(15000)]
        public string Message { get; set; }
    }
}
