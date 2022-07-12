using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Journal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdJournal { get; set; }

        public string Title { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? LastEditDate { get; set; }

        public string? Message { get; set; }


        public string IdUser { get; set; }
        [ForeignKey("IdUser")]
        public virtual ApplicationUser AspNetUsers { get; set; }
    }

}

