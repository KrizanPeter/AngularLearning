using API.Entities.Context;
using BoardGame.Domain.Repositories.Interfaces;
using System.Threading.Tasks;

namespace BoardGame.Domain.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly DataContext _db;
       
       public UnitOfWork(DataContext context)
       {
           _db = context;
           Sessions  = new SessionRepository(_db);
            AppUsers = new AppUserRepository(_db);
           
       }

        public ISessionRepository Sessions {get; private set;}

        public IAppUserRepository AppUsers { get; private set; }

        public async void Dispose()
        {
            await _db.DisposeAsync();
        }

        public async Task<bool> Save()
        {
            return await _db.SaveChangesAsync() > 0;
        }
    }
}