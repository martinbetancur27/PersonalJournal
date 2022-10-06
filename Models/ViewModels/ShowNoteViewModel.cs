
namespace Models.ViewModels
{
    public class ShowNoteViewModel
    {
        public int IdNote { get; set; }

        public int IdJournal { get; set; }

        public string Title { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastEditDate { get; set; }

        public string Message { get; set; }
    }
}
