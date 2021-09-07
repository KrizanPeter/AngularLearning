using System;
using System.Threading.Tasks;
using API.Data.Repositories.IRepositories;

namespace API.Data.Repositories.Uow
{
    public interface IUnitOfWork : IDisposable
    {
        IAppUserRepository AppUsers { get; }
        ISessionRepository Sessions {get;}
        Task<bool> Save();
    }
}