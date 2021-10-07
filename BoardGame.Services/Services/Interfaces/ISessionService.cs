using System.Collections.Generic;
using System.Threading.Tasks;

using BoardGame.Domain.Models;
using BoardGame.Services.ReturnStates;

namespace BoardGame.Services.Services.Interfaces
{
    public interface ISessionService
    {
        Task<OperationalResult<SessionModel>> LoadSessionAsync(int sessionId, int startX, int startY, int endX, int endY);
        Task<OperationalResult> AddSession(int userId, SessionModel sessionModel);
        Task<OperationalResult<IEnumerable<SessionModel>>> GetSessions();
        Task<OperationalResult<SessionModel>> GetSessionById(int id);
        Task<string> ChangeActivePlayer(int sessionId);
    }
}
