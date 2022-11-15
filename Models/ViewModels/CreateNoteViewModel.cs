
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class CreateNoteViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(15000)]
        public string Message { get; set; }
    }
}
