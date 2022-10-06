
namespace Models
{
    public class Note
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdNote { get; set; }

        public string Title { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastEditDate { get; set; }

        public string Message { get; set; }

        public int IdJournal { get; set; }
        //[ForeignKey("IdJournal")]
        public Journal Journal { get; set; }

        public ICollection<Comment>? Comments { get; set; }

    }
}
