
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class CreateNoteViewModel
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        
        [Required]
        [StringLength(15000)]
        public string Message { get; set; }
    }
}
