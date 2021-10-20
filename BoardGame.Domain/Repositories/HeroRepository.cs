using API.Entities.Context;
using AutoMapper;
using BoardGame.Domain.Entities;
using BoardGame.Domain.Models;
using BoardGame.Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Domain.Repositories
{
    public class HeroRepository : Repository<Hero>, IHeroRepository
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;

        public HeroRepository(DataContext db, IMapper mapper) : base(db)
        {
            _db = db;
            _mapper = mapper;
        }

        public HeroModel GetHeroModel(int id)
        {
            var result = _db.Heroes.Where(a => a.HeroId == id).SingleOrDefault();
            return _mapper.Map<HeroModel>(result);
        }

        public HeroModel GetHeroModelByUserId(int userId)
        {
            var result = _db.Heroes.Where(a => a.AppUserId == userId).SingleOrDefault();
            return _mapper.Map<HeroModel>(result);
        }
    }
}
