using BoardGame.Domain.Repositories.Interfaces;
using BoardGame.Services.ReturnStates;
using BoardGame.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Services.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _appUserRepository;
        public AppUserService(IAppUserRepository appUserRepository)
        {
            _appUserRepository = appUserRepository;
        }

        public Task<OperationalResult> AddAppUser(Domain.Entities.AppUser user)
        {
            _appUserRepository.Add(user);
            _appUserRepository.Save();
            return Task.FromResult(OperationalResult.Success());
        }

        public async Task<OperationalResult> AddSessionToUserAsync(int userId, int sessionId)
        {
            var user = await _appUserRepository.GetFirstOrDefault(a => a.Id == userId);
            user.SessionId = sessionId;
            _appUserRepository.Save();
            return OperationalResult.Success();
        }

        public async Task<Domain.Entities.AppUser> GetAppUser(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new Exception("UserName can not be empty");
            }

            var user = await _appUserRepository.GetFirstOrDefault(a => a.UserName == userName);
            return user;
        }

        public async Task<OperationalResult> GetAppUser(int id)
        {
            var result = await _appUserRepository.GetFirstOrDefault(a => a.Id == id);
            return new OperationalResult<Domain.Entities.AppUser>(result);
        }

        public async Task<int> GetAppUserId(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new Exception("UserName can not be empty");
            }

            var user = await _appUserRepository.GetFirstOrDefault(a => a.UserName == userName);
            return user.Id;
        }


    }
}
