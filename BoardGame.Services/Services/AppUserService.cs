using System;
using System.Linq;
using System.Threading.Tasks;

using BoardGame.Domain.Repositories.Interfaces;
using BoardGame.Services.ReturnStates;
using BoardGame.Services.Services.Interfaces;

namespace BoardGame.Services.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IHeroRepository _heroRepository;
        private readonly ISessionRepository _sessionRepository;
        public AppUserService(IAppUserRepository appUserRepository, IHeroRepository heroRepository, ISessionRepository sessionRepository)
        {
            _appUserRepository = appUserRepository;
            _heroRepository = heroRepository;
            _sessionRepository = sessionRepository;
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
            user.JoinedSessionAt = DateTime.UtcNow;
            var session = await _sessionRepository.Get(sessionId);
            if(session.CurrentPlayerId == null)
            {
                session.CurrentPlayerId = userId;
                _sessionRepository.Save();
            }
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

        public async Task<OperationalResult> LeaveSessionForUserAsync(Domain.Entities.AppUser user)
        {
            if(user == null)
            {
                return OperationalResult.Failed();
            }
            user.SessionId = null;
            user.JoinedSessionAt = null;
            var heroEnumerable = await _heroRepository.GetAll(a => a.AppUserId == user.Id);
            var heroList = heroEnumerable.ToList();
            if(heroList != null && heroList.Count > 0)
            {
                foreach( var hero in heroList)
                {
                    _heroRepository.Remove(hero);
                }
                _heroRepository.Save();
            }
            _appUserRepository.Save();

            return OperationalResult.Success();
        }
    }
}
