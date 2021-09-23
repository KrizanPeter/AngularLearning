using API.Entities.Context;
using AutoMapper;
using BoardGame.Domain.Entities;
using BoardGame.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
