using API.Entities.Context;
using AutoMapper;
using BoardGame.Domain.Entities;
using BoardGame.Domain.Models;
using BoardGame.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.Domain.Repositories
{
    public class SessionRepository : Repository<Session>, ISessionRepository
    {
        
        private readonly DataContext _db;
        private readonly IMapper _mapper;

        public SessionRepository(DataContext db, IMapper mapper) : base(db)
        {
            _db = db;
            _mapper = mapper;
        }

        public SessionModel GetSessionWithBlocks(int sessionId)
        {
            var result = _db.Sessions
                .Where(a => a.SessionId == sessionId)
                .Include(a => a.Blocks)
                .SingleOrDefault();

            var sessionModel = _mapper.Map<SessionModel>(result);

            return sessionModel;
        }
    }
}