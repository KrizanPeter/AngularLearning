using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using AutoMapper;

using BoardGame.Domain.Entities;
using BoardGame.Domain.Entities.EntityEnums;
using BoardGame.Domain.Models;
using BoardGame.Domain.Models.Enums;
using BoardGame.Domain.Repositories.Interfaces;
using BoardGame.Services.ReturnStates;
using BoardGame.Services.Services.Interfaces;

namespace BoardGame.Services.Services
{
    public class BlockService : IBlockService
    {
        private readonly IBlockRepository _blockRepository;
        private readonly IBlockTypeRepository _blockTypeRepository;
        private readonly IHeroRepository _heroRepository;
        private readonly IMonsterService _monsterService;
        private readonly IMonsterRepository _monsterRepository;
        private readonly IMapper _mapper;

        public BlockService(IBlockRepository blockRepository,
            IHeroRepository heroRepository,
            IMonsterService monsterService, 
            IMapper mapper, 
            IBlockTypeRepository blockTypeRepository,
            IMonsterRepository monsterRepository)
        {
            _monsterRepository = monsterRepository;
            _blockRepository = blockRepository;
            _heroRepository = heroRepository;
            _mapper = mapper;
            _blockTypeRepository = blockTypeRepository;
            _monsterService = monsterService;
        }

        public Task<OperationalResult<BlockModel>> GetBlockById(int id)
        {
            var block = _blockRepository.Get(id);
            var blockModel = _mapper.Map<BlockModel>(block);
            return Task.FromResult(OperationalResult.Success(blockModel));
        }

        public async Task<OperationalResult<List<BlockModel>>> MoveHeroToBlock(int userId, int targetBlockId)
        {
            // 1 get possible movement and validate requested move
            var hero = await _heroRepository.GetFirstOrDefault(h => h.AppUserId == userId);
            if (hero == null)
            {
                return OperationalResult.Failed<List<BlockModel>>(new OperationalError(HttpStatusCode.NotFound, "Hero was not found for curretn user"));
            }
            var currentBlock = await _blockRepository.GetFirstOrDefault(b => b.BlockId == hero.BlockId, "BlockType");
            var currentBlockModel = _mapper.Map<BlockModel>(currentBlock);
            var targetBlock = await _blockRepository.GetFirstOrDefault(x => x.BlockId == targetBlockId, "BlockType");
            if (targetBlock == null)
            {
                return OperationalResult.Failed<List<BlockModel>>(new OperationalError(HttpStatusCode.NotFound, "Movement target was not found"));
            }
            var targetBlockModel = _mapper.Map<BlockModel>(targetBlock);
            var movement = ValidateMovement(currentBlockModel, targetBlockModel);
            if (movement.InvalidMovement)
            {
                return OperationalResult.Failed<List<BlockModel>>(new OperationalError(HttpStatusCode.BadRequest, "Move not possible"));
            }

            // 2 discover block podla movement variable
            if (targetBlockModel.BlockType.BlockCategory == BlockCategory.Hidden)
            {
                var discoverResult = await DiscoverBlock(targetBlockModel, movement);

                if (discoverResult.Succeeded)
                {
                    if (discoverResult.Data.BlockCategory == BlockCategory.Room)
                    {
                        var monsterGeneratorResult = await _monsterService.GenerateMonster(hero.Level);
                        targetBlock.MonsterId = monsterGeneratorResult.Succeeded ? monsterGeneratorResult.Data.MonsterId : null;
                    }

                    targetBlock.BlockTypeId = targetBlockModel.BlockTypeId;
                    _blockRepository.Save();
                }
                else
                {
                    return OperationalResult.Failed<List<BlockModel>>(new OperationalError(HttpStatusCode.BadRequest, "Failed to discover target block"));
                }
            }
            else
            {
                // validate whether target block can accept this move
                if (movement.ExitDown && !targetBlockModel.BlockType.ExitDown)
                {
                    return OperationalResult.Failed<List<BlockModel>>(new OperationalError(HttpStatusCode.BadRequest, "You can't move through walls"));
                }
                else if (movement.ExitLeft && !targetBlockModel.BlockType.ExitLeft)
                {
                    return OperationalResult.Failed<List<BlockModel>>(new OperationalError(HttpStatusCode.BadRequest, "You can't move through walls"));
                }
                else if (movement.ExitRight && !targetBlockModel.BlockType.ExitRight)
                {
                    return OperationalResult.Failed<List<BlockModel>>(new OperationalError(HttpStatusCode.BadRequest, "You can't move through walls"));
                }
                else if (movement.ExitTop && !targetBlockModel.BlockType.ExitTop)
                {
                    return OperationalResult.Failed<List<BlockModel>>(new OperationalError(HttpStatusCode.BadRequest, "You can't move through walls"));
                }
            }

            // 3 move hero
            hero.BlockId = targetBlockModel.BlockId;
            _heroRepository.Save();

            var blocksToRerender = new List<BlockModel>();
            blocksToRerender.Add(_mapper.Map<BlockModel>(_blockRepository.GetBlockWithHeroesAndMonster(currentBlockModel.BlockId)));
            var updatedTargetBlock = _mapper.Map<BlockModel>(_blockRepository.GetBlockWithHeroesAndMonster(targetBlockModel.BlockId));
            updatedTargetBlock.IncomingMovement = targetBlockModel.IncomingMovement.ToLower();
            blocksToRerender.Add(updatedTargetBlock);

            return OperationalResult.Success(blocksToRerender);
        }

        private MovementModel ValidateMovement(BlockModel currentBlock, BlockModel targetBlock)
        {
            if (targetBlock.BlockPositionX == currentBlock.BlockPositionX + 1
                && targetBlock.BlockPositionY == currentBlock.BlockPositionY
                && currentBlock.BlockType.ExitRight)
            {
                targetBlock.IncomingMovement = MovementSource.Left.ToString();
                return new MovementModel { ExitLeft = true };
            }
            else if (targetBlock.BlockPositionX == currentBlock.BlockPositionX - 1
                && targetBlock.BlockPositionY == currentBlock.BlockPositionY
                && currentBlock.BlockType.ExitLeft)
            {
                targetBlock.IncomingMovement = MovementSource.Right.ToString();
                return new MovementModel { ExitRight = true };
            }
            else if (targetBlock.BlockPositionY == currentBlock.BlockPositionY + 1
                && targetBlock.BlockPositionX == currentBlock.BlockPositionX
                && currentBlock.BlockType.ExitDown)
            {
                targetBlock.IncomingMovement = MovementSource.Top.ToString();
                return new MovementModel { ExitTop = true };
            }
            else if (targetBlock.BlockPositionY == currentBlock.BlockPositionY - 1
                && targetBlock.BlockPositionX == currentBlock.BlockPositionX
                && currentBlock.BlockType.ExitTop)
            {
                targetBlock.IncomingMovement = MovementSource.Down.ToString();
                return new MovementModel { ExitDown = true };
            }
            return new MovementModel();
        }

        private async Task<OperationalResult<BlockType>> DiscoverBlock(BlockModel targetBlock, MovementModel movement)
        {
            var rand = new Random();
            var targetCategory = (BlockCategory)rand.Next(1, 3);
            IEnumerable<BlockType> blockTypes = null;
            if (movement.ExitDown)
            {
                blockTypes = await _blockTypeRepository.GetAll(t => t.ExitDown && t.BlockCategory == targetCategory);
            }
            else if (movement.ExitLeft)
            {
                blockTypes = await _blockTypeRepository.GetAll(t => t.ExitLeft && t.BlockCategory == targetCategory);
            }
            else if (movement.ExitRight)
            {
                blockTypes = await _blockTypeRepository.GetAll(t => t.ExitRight && t.BlockCategory == targetCategory);
            }
            else if (movement.ExitTop)
            {
                blockTypes = await _blockTypeRepository.GetAll(t => t.ExitTop && t.BlockCategory == targetCategory);
            }

            if(blockTypes != null && blockTypes.Any())
            {
                var blockTypeList = blockTypes.ToList();
                var selectedBlockType = blockTypeList.ElementAt(rand.Next(0, blockTypeList.Count));
                targetBlock.BlockTypeId = selectedBlockType.BlockTypeId;
                return OperationalResult.Success(selectedBlockType);
            }
            return OperationalResult.Failed<BlockType>();
        }

        public async Task<OperationalResult<BattleReportModel>> ResolveConflictOnBlock(int blockId, int attackerUserId)
        {
            var blockModel = _blockRepository.GetBlockModelWithHeroesAndMonster(blockId);
            if (blockModel == null) return OperationalResult.Failed<BattleReportModel>(new OperationalError(HttpStatusCode.NotFound, "Block was not found"));
            BattleReportModel model = null;
            if (blockModel.Heroes.Count == 2)
            {
                model = await ResolvePvPFightAsync(blockModel, attackerUserId);
            }
            else if(blockModel.Monster != null && blockModel.Heroes.Any())
            {
                model = await ResolvePveFightAsync(blockModel, attackerUserId);
            }

            return OperationalResult.Success(model);
        }

        private async Task<BattleReportModel> ResolvePvPFightAsync(BlockModel blockModel, int attackerUserId)
        {
            var attackerHero = _heroRepository.GetHeroModelByUserId(attackerUserId);
            var defenderHeroId = blockModel.Heroes.First(a => a.AppUserId != attackerUserId).HeroId;
            var defenderhero = _heroRepository.GetHeroModel(defenderHeroId);
            Random rand = new Random();
            var attackerDmg = 0;
            var defenderDmg = 0;
            var winner = "Nobody";

            for (int i = 0; i<5; i++)
            {
                var attackerHit = rand.Next(attackerHero.DmgMin, attackerHero.DmgMax + 1);
                var defenderHit = rand.Next(defenderhero.DmgMin, defenderhero.DmgMax + 1);
                var attackerCalculatedHit = attackerHit - defenderhero.Armor;
                var defenderCalculatedHit = defenderHit - attackerHero.Armor;
                attackerDmg += attackerCalculatedHit;
                defenderDmg += defenderCalculatedHit;
                if(defenderhero.Lives - attackerDmg <= 0)
                {
                    winner = defenderhero.HeroName;
                    break;
                }
                if(attackerHero.Lives - defenderDmg <= 0)
                {
                    winner = attackerHero.HeroName;
                    break;
                }
            }

            var attackerHeroEntity = await _heroRepository.GetFirstOrDefault(a => a.AppUserId == attackerUserId);
            var defenderheroEntity = await _heroRepository.Get(defenderHeroId);
            attackerHeroEntity.Lives -= defenderDmg;
            defenderheroEntity.Lives -= attackerDmg;
            attackerHeroEntity.Experience += attackerHeroEntity.Level * 10;
            defenderheroEntity.Experience += attackerHeroEntity.Level * 10;
            attackerHeroEntity = ConsolidatePlayerAfterFight(attackerHeroEntity);
            defenderheroEntity = ConsolidatePlayerAfterFight(defenderheroEntity);
            _heroRepository.Save();

            var report = ConstructPvpReport(winner, attackerHero, attackerDmg, defenderhero, defenderDmg);

            return report;
        }

        private Hero ConsolidatePlayerAfterFight(Hero hero)
        {
            if (hero.Lives < 0) hero.Lives = 0;
            if (hero.Experience >= hero.ExperienceCap)
            {
                hero.Level++;
                hero.Experience = hero.Experience - hero.ExperienceCap;
                hero.ExperienceCap = (int)(hero.ExperienceCap * 1.2);
                hero.SkillPoints += 3;
            }
            return hero;
        }

        private BattleReportModel ConstructPvpReport(string winner, HeroModel attackerHero, int attackerDmg, HeroModel defenderhero, int defenderDmg)
        {
            var reportTitle = "Battle result: " + winner + " has been killed!";
            return new BattleReportModel(reportTitle, "imga", "imgd", attackerHero.HeroName,
                defenderhero.HeroName, attackerHero.LivesCap,
                attackerHero.Lives - defenderDmg, defenderhero.LivesCap,
                defenderhero.Lives - attackerDmg, attackerHero.DmgMin, defenderhero.DmgMin, 
                attackerHero.DmgMax, defenderhero.DmgMax,
                attackerHero.Armor, defenderhero.Armor, attackerHero.Level * 10);
        }

        private BattleReportModel ConstructPveReport(string winner, HeroModel attackerHero, int attackerDmg, MonsterModel monster, int defenderDmg)
        {
            var reportTitle = "Battle result: " + winner + " has been killed!";
            return new BattleReportModel(reportTitle, "imga", "imgd", attackerHero.HeroName,
                monster.MonsterName, attackerHero.LivesCap,
                attackerHero.Lives - defenderDmg, monster.Life,
                monster.Life - attackerDmg, attackerHero.DmgMin, monster.DmgMin,
                attackerHero.DmgMax, monster.DmgMax,
                attackerHero.Armor, monster.Armor, attackerHero.Level * 10);
        }

        private async Task<BattleReportModel> ResolvePveFightAsync(BlockModel blockModel, int attackerUserId)
        {
            var attackerHero = _heroRepository.GetHeroModelByUserId(attackerUserId);
            var defenderMonster = _monsterRepository.GetMonsterModel(blockModel.MonsterId ?? default(int));

            Random rand = new Random();
            var attackerDmg = 0;
            var defenderDmg = 0;
            var winner = "Nobody";

            for (int i = 0; i < 5; i++)
            {
                var attackerHit = rand.Next(attackerHero.DmgMin, attackerHero.DmgMax + 1);
                var defenderHit = rand.Next(defenderMonster.DmgMin, defenderMonster.DmgMax + 1);
                var attackerCalculatedHit = attackerHit - defenderMonster.Armor;
                var defenderCalculatedHit = defenderHit - attackerHero.Armor;
                attackerDmg += attackerCalculatedHit;
                defenderDmg += defenderCalculatedHit;
                if (defenderMonster.Life - attackerDmg <= 0)
                {
                    winner = defenderMonster.MonsterName;
                    break;
                }
                if (attackerHero.Lives - defenderDmg <= 0)
                {
                    winner = attackerHero.HeroName;
                    break;
                }
            }

            var attackerHeroEntity = await _heroRepository.GetFirstOrDefault(a => a.AppUserId == attackerUserId);
            attackerHeroEntity.Lives -= defenderDmg;

            attackerHeroEntity.Experience += attackerHeroEntity.Level * 10;
            attackerHeroEntity = ConsolidatePlayerAfterFight(attackerHeroEntity);
            var consolidatedModel = await ConsolidateMonsterAfterFightAsync(defenderMonster, attackerDmg);
            _heroRepository.Save();

            var report = ConstructPveReport(winner, attackerHero, attackerDmg, consolidatedModel, defenderDmg);
            return report;
        }

        private async Task<MonsterModel> ConsolidateMonsterAfterFightAsync(MonsterModel monster, int attackerDmg)
        {
            if (monster.Life - attackerDmg <= 0)
            {
                monster.Life = 0;
                _monsterRepository.Remove(monster.MonsterId);
                _monsterRepository.Save();
                return monster;
            }

            var monsterEntity = await _monsterRepository.Get(monster.MonsterId);
            monsterEntity.Life -= attackerDmg;
            _monsterRepository.Save();

            var monsterModel = _monsterRepository.GetMonsterModel(monster.MonsterId);
            

            return monsterModel;
        }
    }
}
