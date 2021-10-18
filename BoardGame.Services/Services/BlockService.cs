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
        private readonly IMapper _mapper;

        public BlockService(IBlockRepository blockRepository, IHeroRepository heroRepository, IMonsterService monsterService, IMapper mapper, IBlockTypeRepository blockTypeRepository)
        {
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
            blocksToRerender.Add(_mapper.Map<BlockModel>(_blockRepository.GetBlockWithHeroes(currentBlockModel.BlockId)));
            var updatedTargetBlock = _mapper.Map<BlockModel>(_blockRepository.GetBlockWithHeroes(targetBlockModel.BlockId));
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
    }
}
