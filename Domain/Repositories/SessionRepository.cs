using API.Entities.Context;
using BoardGame.Domain.Entities;
using BoardGame.Domain.Repositories.Interfaces;

namespace BoardGame.Domain.Repositories
{
    public class SessionRepository : Repository<GameSession>, ISessionRepository
    {
        
        private readonly DataContext _db;

        public SessionRepository(DataContext db) : base(db)
        {
            _db = db;
        }
    }
}