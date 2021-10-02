using API.Entities.Context;
using AutoMapper;
using BoardGame.Domain.Entities;
using BoardGame.Domain.Entities.EntityEnums;
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

        public SessionModel GetSessionWithBlocks(int sessionId, int startX, int startY, int endX, int endY)
        {
            var result = _db.Sessions
                .Where(a => a.SessionId == sessionId)
                .Include(a => a.Blocks)
                .ThenInclude(a=>a.BlockType)
                .Include(b=>b.Blocks)
                .ThenInclude(a=>a.Heroes)
                .Select(
                c => new Session()
                {
                    Blocks = c.Blocks.Where(a => (a.BlockPositionX >= startX && a.BlockPositionX <= endX) && (a.BlockPositionY >= startY && a.BlockPositionY <= endY)).OrderBy(a=>a.BlockOrder).ToList(),
                    CenterBlockPosition = c.CenterBlockPosition,
                    PlanSize = c.PlanSize,
                    SessionId = c.SessionId,
                    SessionName = c.SessionName,
                    SessionPassword = c.SessionPassword,
                    SessionType = c.SessionType,

                })
                .SingleOrDefault();

            var sessionModel = _mapper.Map<SessionModel>(result);

            return sessionModel;
        }

        public PlanSize GetSizeOfSession(int? sessionId)
        {
            var result = _db.Sessions
                .Where(a => a.SessionId == sessionId)
                .SingleOrDefault();

            return result.PlanSize;
        }
    }
}