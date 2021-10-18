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
    public class MonsterRepository : Repository<Monster>, IMonsterRepository
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;

        public MonsterRepository(DataContext db, IMapper mapper) : base(db)
        {
            _db = db;
            _mapper = mapper;
        }
    }
}
