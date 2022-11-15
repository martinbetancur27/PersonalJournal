
using System.ComponentModel.DataAnnotations;

namespace Models.ViewModels
{
    public class CreateCommentViewModel
    {
        [Required]
        [MaxLength(800)]
        public string Message { get; set; }
    }
}
