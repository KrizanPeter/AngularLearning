using BoardGame.Domain.Entities;
using BoardGame.Domain.Entities.EntityEnums;
using BoardGame.Domain.Models;
using BoardGame.Domain.Models.Enums;
using BoardGame.Services.ReturnStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Services.Services.Interfaces
{
    public interface IHeroService
    {
        Task<OperationalResult> CreateHero(AppUser user, HeroType heroType);
        Task<OperationalResult<HeroModel>> GetHeroInformationOfUser(int id);
        Task<OperationalResult> UpgradeAttributeOfUserHero(int id, HeroAttribute attribute);
        Task HealAllHeroesEOR();
    }
}
