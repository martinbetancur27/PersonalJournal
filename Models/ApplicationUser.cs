﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Journal>? Journals { get; set; }
    }
}
