using API.Entities.Context;
using AutoMapper;
using BoardGame.Domain.Entities;
using BoardGame.Domain.Models;
using BoardGame.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Domain.Repositories
{
    public class MonsterRepository : Repository<Monster>, IMonsterRepository
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;

        public MonsterRepository(DataContext db, IMapper mapper) : base(db)
        {
            _db = db;
            _mapper = mapper;
        }

        public MonsterModel GetMonsterModel(int monsterId)
        {
            var result = _db.Monsters.Where(a => a.MonsterId == monsterId).Include(a => a.MonsterType).SingleOrDefault();
            var resultModel = _mapper.Map<MonsterModel>(result);
            return resultModel;
        }
    }
}
