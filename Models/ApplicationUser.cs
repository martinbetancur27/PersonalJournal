using Microsoft.AspNetCore.Identity;

namespace Models
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<Journal>? Journals { get; set; }
    }
}
