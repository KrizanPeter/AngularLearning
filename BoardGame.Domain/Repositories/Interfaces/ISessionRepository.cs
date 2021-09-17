using API.Entities;
using BoardGame.Domain.Entities;
using BoardGame.Domain.Models;
using System.Threading.Tasks;

namespace BoardGame.Domain.Repositories.Interfaces
{
    public interface ISessionRepository : IRepository<Session>
    {
        SessionModel GetSessionWithBlocks(int sessionId, int startX, int startY, int endX, int endY);
    }
}