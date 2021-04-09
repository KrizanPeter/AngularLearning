using System;
using API.Data.Repositories.IRepositories;

namespace API.Data.Repositories.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        ISessionRepository Sessions {get;}
        void Save();
    }
}