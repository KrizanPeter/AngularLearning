using System;
using API.Data.Repositories.IRepositories;

namespace API.Data.Repositories.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        IAppUserRepository AppUser {get;}
        void Save();
    }
}