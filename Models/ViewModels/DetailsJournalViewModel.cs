﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
