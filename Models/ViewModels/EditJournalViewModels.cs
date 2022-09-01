using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class EditJournalViewModels
    {
        public int IdJournal { get; set; }
        public string Title { get; set; }
        public string? Message { get; set; }
    }
}
