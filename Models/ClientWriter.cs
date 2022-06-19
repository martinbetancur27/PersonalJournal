using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class ClientWriter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdClientWriter { get; set; }

        public int IdAccount { get; set; }
        [ForeignKey("IdAccount")]

        public virtual Account Account { get; set; }
    }
}
