using BoardGame.Domain.Entities;
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
        Task<bool> AddServiceToUserAsync(int userId, int sessionId);
    }
}
