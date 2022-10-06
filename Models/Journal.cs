
namespace Models
{
    public class Journal
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdJournal { get; set; }

        public string Title { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastEditDate { get; set; }

        public string? Message { get; set; }


        public string IdUser { get; set; }
        //[ForeignKey("IdUser")]
        public ApplicationUser AspNetUsers { get; set; }

        public ICollection<Note>? Notes { get; set; }
    }

}

