using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Comment
    {
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdComment { get; set; }

        public DateTime CreateDate { get; set; }

        public string Message { get; set; }

        public int IdNote { get; set; }
        //[ForeignKey("IdNote")]
        public Note Note { get; set; }


        
    }
}
