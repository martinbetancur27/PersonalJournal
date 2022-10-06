
namespace Models.ViewModels
{
    public class DetailsJournalViewModel
    {
        public int IdJournal { get; set; }
        public string Title { get; set; }
        public string? Message { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? LastEditDate { get; set; }
    }
}
