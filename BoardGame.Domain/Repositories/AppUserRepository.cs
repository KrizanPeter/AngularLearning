using API.Entities.Context;
using BoardGame.Domain.Entities;
using BoardGame.Domain.Repositories.Interfaces;

namespace BoardGame.Domain.Repositories
{
    public class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {

        private readonly DataContext _db;

        public AppUserRepository(DataContext db) : base(db)
        {
            _db = db;
        }
    }
}
