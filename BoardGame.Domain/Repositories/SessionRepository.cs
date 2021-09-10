using API.Entities.Context;
using BoardGame.Domain.Entities;
using BoardGame.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.Domain.Repositories
{
    public class SessionRepository : Repository<Session>, ISessionRepository
    {
        
        private readonly DataContext _db;

        public SessionRepository(DataContext db) : base(db)
        {
            _db = db;
        }

        public Session GetSessionWithBlocks(int sessionId)
        {
            var result = _db.Sessions
                .Where(a => a.SessionId == sessionId)
                .Include(a => a.Blocks)
                .SingleOrDefault();

            return result;
        }
    }
}