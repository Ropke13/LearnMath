using Microsoft.AspNetCore.Identity;

namespace Bakalauras.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Role { get; set; }

    }
}
