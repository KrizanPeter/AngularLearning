using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;

namespace BoardGame.Domain.Entities
{
    public class AppUser : IdentityUser<int>
    {
        public int? SessionId { get; set; }
        public DateTime? JoinedSessionAt { get; set; }
        public virtual ICollection<AppUserRole> UserRoles { get; set; }
        public virtual Session Session { get; set; }
    }
}
