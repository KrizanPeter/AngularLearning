using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public int? GameSessionId { get; set; }
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
        public virtual GameSession GameSession { get; set; }

    }
}
