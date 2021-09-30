using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

using AutoMapper;

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
        private readonly IHeroRepository _heroRepository;
        private readonly IMapper _mapper;

        public BlockService(IBlockRepository blockRepository, IHeroRepository heroRepository, IMapper mapper)
        {
            _blockRepository = blockRepository;
            _heroRepository = heroRepository;
            _mapper = mapper;
        }

        public Task<OperationalResult<BlockModel>> GetBlockById(int id)
        {
            var block = _blockRepository.Get(id);
            var blockModel = _mapper.Map<BlockModel>(block);
            return Task.FromResult(OperationalResult.Success(blockModel));
        }

        public async Task<OperationalResult<List<BlockModel>>> MoveHeroToBlock(int userId, int targetBlockId)
        {
            // 1 check validity
            var hero = await _heroRepository.GetFirstOrDefault(h => h.AppUserId == userId);
            if(hero == null)
            {
                return OperationalResult.Failed<List<BlockModel>>(new OperationalError(HttpStatusCode.NotFound, "Hero was not found for curretn user"));
            }
            var targetBlock = await _blockRepository.Get(targetBlockId);
            if(targetBlock == null)
            {
                return OperationalResult.Failed<List<BlockModel>>(new OperationalError(HttpStatusCode.NotFound, "Movement target was not found"));
            }
            var targetBlockModel = _mapper.Map<BlockModel>(targetBlock);
            var currentBlock = await _blockRepository.Get(hero.BlockId);
            var currentBlockModel = _mapper.Map<BlockModel>(currentBlock);
            var movement = ValidateMovement(currentBlockModel, targetBlockModel);
            if(movement.InvalidMovement)
            {
                return OperationalResult.Failed<List<BlockModel>>(new OperationalError(HttpStatusCode.BadRequest, "Move not possible"));
            }

            // 2 discover block
            if(targetBlockModel.BlockType == BlockType.Hidden)
            {
                DiscoverBlock(targetBlockModel, movement);
                targetBlock.BlockType = targetBlockModel.BlockType;
                targetBlock.BlockDirection = targetBlockModel.BlockDirection;
                targetBlock.ImagePath = targetBlockModel.ImagePath;
                _blockRepository.Save();
            }

            // 3 move hero
            hero.BlockId = targetBlockModel.BlockId;
            _heroRepository.Save();

            var blocksToRerender = new List<BlockModel>();
            blocksToRerender.Add(_mapper.Map<BlockModel>(_blockRepository.GetBlockWithHeroes(currentBlockModel.BlockId)));
            blocksToRerender.Add(_mapper.Map<BlockModel>(_blockRepository.GetBlockWithHeroes(targetBlockModel.BlockId)));

            return OperationalResult.Success(blocksToRerender);
        }

        private MovementModel ValidateMovement(BlockModel currentBlock, BlockModel targetBlock)
        {
            if(targetBlock.BlockPositionX == currentBlock.BlockPositionX + 1)
            {
                return new MovementModel(MovementSource.Left);
            }
            if(targetBlock.BlockPositionX == currentBlock.BlockPositionX - 1)
            {
                return new MovementModel(MovementSource.Right);
            }
            if(targetBlock.BlockPositionY == currentBlock.BlockPositionY + 1)
            {
                return new MovementModel(MovementSource.Down);
            }
            if(targetBlock.BlockPositionY == currentBlock.BlockPositionY - 1)
            {
                return new MovementModel(MovementSource.Top);
            }
            return new MovementModel();
        }

        private void DiscoverBlock(BlockModel targetBlock, MovementModel movement)
        {
            var rand = new Random();
            targetBlock.BlockType = (BlockType)rand.Next(1, 3);
            targetBlock.BlockDirection = movement.PossibleDirections.ElementAt(rand.Next(0, movement.PossibleDirections.Count));

            if (targetBlock.BlockType == BlockType.Room)
            {
                if(targetBlock.BlockDirection == BlockDirection.Cross)
                {
                    targetBlock.ImagePath = "url(assets/images/gameboard/gameblock-X-room.png)";
                }
                if (targetBlock.BlockDirection == BlockDirection.DownLeft)
                {
                    targetBlock.ImagePath = "url(assets/images/gameboard/gameblock-room-downleft.png)";
                }
                if (targetBlock.BlockDirection == BlockDirection.DownRight)
                {
                    targetBlock.ImagePath = "url(assets/images/gameboard/gameblock-room-downright.png)";
                }
                if (targetBlock.BlockDirection == BlockDirection.Horizontal)
                {
                    targetBlock.ImagePath = "url(assets/images/gameboard/gameblock-room-horizontal.png)";
                }
                if (targetBlock.BlockDirection == BlockDirection.TopLeft)
                {
                    targetBlock.ImagePath = "url(assets/images/gameboard/gameblock-room-topleft.png)";
                }
                if (targetBlock.BlockDirection == BlockDirection.TopRight)
                {
                    targetBlock.ImagePath = "url(assets/images/gameboard/gameblock-room-topright.png)";
                }
                if (targetBlock.BlockDirection == BlockDirection.Vertical)
                {
                    targetBlock.ImagePath = "url(assets/images/gameboard/gameblock-room-vertical.png)";
                }
            }
            else if(targetBlock.BlockType == BlockType.Hall)
            {
                if (targetBlock.BlockDirection == BlockDirection.Cross)
                {
                    targetBlock.ImagePath = "url(assets/images/gameboard/gameblock-X.png)";
                }
                if (targetBlock.BlockDirection == BlockDirection.DownLeft)
                {
                    targetBlock.ImagePath = "url(assets/images/gameboard/gameblock-hall-downleft.png)";
                }
                if (targetBlock.BlockDirection == BlockDirection.DownRight)
                {
                    targetBlock.ImagePath = "url(assets/images/gameboard/gameblock-hall-downright.png)";
                }
                if (targetBlock.BlockDirection == BlockDirection.Horizontal)
                {
                    targetBlock.ImagePath = "url(assets/images/gameboard/gameblock-hall-horizontal.png)";
                }
                if (targetBlock.BlockDirection == BlockDirection.TopLeft)
                {
                    targetBlock.ImagePath = "url(assets/images/gameboard/gameblock-hall-topleft.png)";
                }
                if (targetBlock.BlockDirection == BlockDirection.TopRight)
                {
                    targetBlock.ImagePath = "url(assets/images/gameboard/gameblock-hall-topright.png)";
                }
                if (targetBlock.BlockDirection == BlockDirection.Vertical)
                {
                    targetBlock.ImagePath = "url(assets/images/gameboard/gameblock-hall-vertical.png)";
                }
            }
        }
    }
}
