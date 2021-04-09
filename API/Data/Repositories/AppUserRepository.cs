using API.Data.Repositories.IRepositories;
using API.Entities;

namespace API.Data.Repositories
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