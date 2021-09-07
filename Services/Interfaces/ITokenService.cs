using API.Entities;
using BoardGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.Service.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
