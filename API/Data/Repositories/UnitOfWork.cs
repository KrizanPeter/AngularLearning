using API.Data.Repositories.IRepositories;

namespace API.Data.Repositories.Uow
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly DataContext _db;
       
       public UnitOfWork(DataContext context)
       {
           _db = context;
           AppUser  = new AppUserRepository(_db);
           
       }

        public IAppUserRepository AppUser {get; private set;}

        public async void Dispose()
        {
            await _db.DisposeAsync();
        }

        public async void Save()
        {
            await _db.SaveChangesAsync();
        }
    }
}