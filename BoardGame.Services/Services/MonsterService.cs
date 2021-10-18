using AutoMapper;
using BoardGame.Domain.Entities;
using BoardGame.Domain.Models;
using BoardGame.Domain.Repositories.Interfaces;
using BoardGame.Services.ReturnStates;
using BoardGame.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Services.Services
{
    public class MonsterService: IMonsterService
    {
        public readonly IMonsterRepository _monsterRepository;
        public readonly IMonsterTypeRepository _monsterTypeRepository;
        public readonly IAppUserService _appUserService;
        public readonly IMapper _mapper;

        public MonsterService(IMonsterRepository monsterRepository, IMonsterTypeRepository monsterTypeRepository, IAppUserService appUserService, IMapper mapper)
        {
            _monsterRepository = monsterRepository;
            _monsterTypeRepository = monsterTypeRepository;
            _appUserService = appUserService;
            _mapper = mapper;
        }

        public async Task<OperationalResult<MonsterModel>> GenerateMonster(int heroLvl)
        {
            var rand = new Random();
            var monsterTypes = await _monsterTypeRepository.GetAll();

            var type = monsterTypes.ElementAt(rand.Next(0, monsterTypes.Count()));
            var levelOfMonster = rand.Next(heroLvl+1, heroLvl + 10);
            var monster = new Monster()
            {
                MonsterType = type,
                Level = levelOfMonster,
                DmgMin = 1 + rand.Next(0, levelOfMonster),
                DmgMax = 12 + rand.Next(0, levelOfMonster),
                Armor = rand.Next(1, levelOfMonster),
                Life = 10 + (rand.Next(0, levelOfMonster)) * 5,
                MonsterName = "Monster"
            };

            _monsterRepository.Add(monster);
            _monsterRepository.Save();

            return OperationalResult.Success(new MonsterModel { MonsterId = monster.MonsterId });
        }
    }
}
