using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class ShowNoteAndCommentsViewModel
    {
        public ShowNoteViewModel Note { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
