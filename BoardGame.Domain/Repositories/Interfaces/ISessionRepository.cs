using API.Entities;
using BoardGame.Domain.Entities;
using System.Threading.Tasks;

namespace BoardGame.Domain.Repositories.Interfaces
{
    public interface ISessionRepository : IRepository<Session>
    {
        Session GetSessionWithBlocks(int sessionId);
    }
}