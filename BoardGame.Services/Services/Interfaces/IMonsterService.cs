using BoardGame.Domain.Entities;
using BoardGame.Domain.Models;
using BoardGame.Services.ReturnStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Services.Services.Interfaces
{
    public interface IMonsterService
    {
        Task<OperationalResult<MonsterModel>> GenerateMonster(int heroLvl);
    }
}
