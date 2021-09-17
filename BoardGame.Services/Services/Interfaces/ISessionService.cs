using BoardGame.Domain.Entities;
using BoardGame.Domain.Models;
using BoardGame.Services.ReturnStates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Services.Services.Interfaces
{
    public interface ISessionService
    {
        Task<OperationalResult<SessionModel>> LoadSessionAsync(int sessionId, int startX, int startY, int endX, int endY);
        Task<OperationalResult> AddSession(int userId, SessionModel session);
        Task<OperationalResult<IEnumerable<SessionModel>>> GetSessions();
        Task<OperationalResult<SessionModel>> GetSessionById(int id);
    }
}
