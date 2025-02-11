﻿using System.Linq;

using API.Entities.Context;

using AutoMapper;

using BoardGame.Domain.Entities;
using BoardGame.Domain.Models;
using BoardGame.Domain.Repositories.Interfaces;

using Microsoft.EntityFrameworkCore;

namespace BoardGame.Domain.Repositories
{
    public class BlockRepository : Repository<Block>, IBlockRepository
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;

        public BlockRepository(DataContext db, IMapper mapper) : base(db)
        {
            _db = db;
            _mapper = mapper;
        }

        public Block GetCenterBlock(int sessionId, int centerBlockPosition)
        {
            var result = _db.Blocks.Where(a => a.SessionId == sessionId && a.BlockOrder == centerBlockPosition).SingleOrDefault();
            return result;
        }

        public Block GetBlockWithHeroesAndMonster(int blockId)
        {
            var result = _db.Blocks.Where(b => b.BlockId == blockId).Include(x => x.Heroes).Include(a=>a.Monster).ThenInclude(a=>a.MonsterType).SingleOrDefault();
            return result;
        }

        public BlockModel GetBlockModelWithHeroesAndMonster(int blockId)
        {
            var result = _db.Blocks.Where(b => b.BlockId == blockId).Include(x => x.Heroes).Include(a => a.Monster).ThenInclude(a => a.MonsterType).SingleOrDefault();
            return _mapper.Map<BlockModel>(result);
        }
    }
}
