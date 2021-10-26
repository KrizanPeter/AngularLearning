using AutoMapper;
using BoardGame.Domain.Entities;
using BoardGame.Domain.Entities.EntityEnums;
using BoardGame.Domain.Models;
using BoardGame.Domain.Models.Enums;
using BoardGame.Domain.Repositories.Interfaces;
using BoardGame.Services.ReturnStates;
using BoardGame.Services.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Services.Services
{
    public class HeroService : IHeroService
    {
        private readonly ILogger<HeroService> _logger;
        private readonly IAppUserService _appUserService;
        private readonly IHeroRepository _heroRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IBlockRepository _blockRepository;
        private readonly IMapper _mapper;

        public HeroService(ILogger<HeroService> logger,
            IAppUserService appUserService,
            IHeroRepository heroRepository,
            IMapper mapper,
            ISessionRepository sessionRepository,
            IBlockRepository blockRepository)
        {
            _logger = logger;
            _appUserService = appUserService;
            _heroRepository = heroRepository;
            _sessionRepository = sessionRepository;
            _mapper = mapper;
            _blockRepository = blockRepository;
        }

        public async Task<OperationalResult> CreateHero(AppUser user, HeroType heroType)
        {
            var planSize = _sessionRepository.GetSizeOfSession(user.SessionId);
            var centerBlockPosition = ((int)planSize * (int)planSize) / 2 + 1;
            var sessionId = user.SessionId ?? default(int);
            var centerBlock = _blockRepository.GetCenterBlock(sessionId, centerBlockPosition);
            var path = GetPathToImage(heroType);

            var hero = new Hero()
            {
                HeroName = user.UserName,
                AppUserId = user.Id,
                HeroType = heroType,
                Lives = 100,
                BlockId = centerBlock.BlockId,
                ImagePath = path
            };
            _heroRepository.Add(hero);
            _heroRepository.Save();
            return OperationalResult.Success();
        }

        public async Task<OperationalResult<HeroModel>> GetHeroInformationOfUser(int id)
        {
            var hero = await _heroRepository.GetFirstOrDefault(a=>a.AppUserId == id);
            var heroModel = _mapper.Map<HeroModel>(hero);
            if (heroModel == null)
            {
                return OperationalResult.Failed<HeroModel>();
            }
            return OperationalResult.Success(heroModel);
        }

        public async Task HealAllHeroesEOR()
        {
            var heroes = await _heroRepository.GetAll();
            foreach(var hero in heroes)
            {
                var heal = (int)(0.2 * hero.LivesCap);
                hero.Lives += heal;
                if (hero.Lives>hero.LivesCap)
                {
                    hero.Lives = hero.LivesCap;
                }
                
                _heroRepository.Save();
            }
        }

        public async Task<OperationalResult> UpgradeAttributeOfUserHero(int id, HeroAttribute attribute)
        {
            var hero = await _heroRepository.GetFirstOrDefault(a => a.AppUserId == id);
            if (hero.SkillPoints <= 0)
            {
                return OperationalResult.Failed(new OperationalError(System.Net.HttpStatusCode.BadRequest, "You have no enough free skill points"));
            }
            if(attribute == HeroAttribute.ARMOR)
            {
                hero.Armor++;
            }
            else if (attribute == HeroAttribute.HP)
            {
                hero.LivesCap +=10;
            }
            else if (attribute == HeroAttribute.DMG)
            {
                hero.DmgMin++;
                hero.DmgMax++;
            }
            hero.SkillPoints--;
            _heroRepository.Save();
            return OperationalResult.Success();
        }

        private string GetPathToImage(HeroType heroType)
        {
            if (heroType == HeroType.Oracle)
                return "url(assets/images/gameboard/hero/oraclehead.png)";
            if (heroType == HeroType.Warlock)
                return "url(assets/images/gameboard/hero/warlockhead.png)";
            if (heroType == HeroType.Swordsman)
                return "url(assets/images/gameboard/hero/swordsmanhead.png)";
            if (heroType == HeroType.Thief)
                return "url(assets/images/gameboard/hero/thiefhead.png)";
            return "";
        }
    }
}
