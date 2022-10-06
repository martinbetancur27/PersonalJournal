
namespace Models.ViewModels
{
    public class ShowNoteAndCommentsViewModel
    {
        public ShowNoteViewModel Note { get; set; }
        public IEnumerable<Comment>? Comments { get; set; }
    }
}
