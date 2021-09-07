using API.Data.Repositories.IRepositories;
using API.Entities;

namespace API.Data.Repositories
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