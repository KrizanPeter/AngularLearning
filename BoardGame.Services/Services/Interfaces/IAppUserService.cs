using BoardGame.Domain.Entities;
using BoardGame.Services.ReturnStates;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Services.Services.Interfaces
{
    public interface IAppUserService
    {
        Task<int> GetAppUserId(string userName);
        Task<AppUser> GetAppUser(string userName);
        Task<OperationalResult> GetAppUser(int id);
        Task<OperationalResult> AddSessionToUserAsync(int userId, int sessionId);
        Task<OperationalResult> AddAppUser(AppUser user);
        Task<OperationalResult> LeaveSessionForUserAsync(AppUser user);
    }
}
