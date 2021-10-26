using BoardGame.Domain.Entities;
using BoardGame.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Domain.Repositories.Interfaces
{
    public interface IMonsterRepository : IRepository<Monster>
    {
        MonsterModel GetMonsterModel(int v);
    }
}
