using BoardGame.Domain.Repositories.Interfaces;
using BoardGame.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Services.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IUnitOfWork _uow;
        public AppUserService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<bool> AddServiceToUserAsync(int userId, int sessionId)
        {
            var user = await _uow.AppUsers.GetFirstOrDefault(a => a.Id == userId);
            user.GameSessionId = sessionId;
            return await _uow.Save();
        }

        public async Task<int> GetAppUserId(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new Exception("UserName can not be empty");
            }

            var user = await _uow.AppUsers.GetFirstOrDefault(a => a.UserName == userName);
            return user.Id;
        }


    }
}
