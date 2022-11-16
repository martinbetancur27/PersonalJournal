
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class CreateCommentViewModel
    {
        [Required]
        [StringLength(800)]
        public string Message { get; set; }
    }
}
