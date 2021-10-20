using BoardGame.Domain.Entities;
using BoardGame.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Domain.Repositories.Interfaces
{
    public interface IHeroRepository : IRepository<Hero>
    {
        HeroModel GetHeroModel(int id);
        HeroModel GetHeroModelByUserId(int userId);
    }
}
