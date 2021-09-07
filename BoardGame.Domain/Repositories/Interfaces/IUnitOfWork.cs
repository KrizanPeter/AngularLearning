using System;
using System.Threading.Tasks;

namespace BoardGame.Domain.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAppUserRepository AppUsers { get; }
        ISessionRepository Sessions {get;}
        Task<bool> Save();
    }
}