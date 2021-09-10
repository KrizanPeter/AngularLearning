using BoardGame.Domain.Entities;
using BoardGame.Domain.Repositories.Interfaces;
using BoardGame.Services.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Services.Services
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _uow;
        private readonly ILogger<SessionService> _logger;
        private readonly IAppUserService _appUserService;
        private readonly ISessionRepository _sessionRepository;

        public SessionService(IUnitOfWork uow, ILogger<SessionService> logger, IAppUserService appUserService, ISessionRepository sessionRepository)
        {
            _uow = uow;
            _logger = logger;
            _appUserService = appUserService;
            _sessionRepository = sessionRepository;
        }

        public Session LoadOrFillSessionAsync(int sessionId)
        {
            var session = _sessionRepository.GetSessionWithBlocks(sessionId);
            if(session.Blocks == null || session.Blocks.Count == 0)
            {
                GenerateBlocksForSession(session);
            }
            return session;
        }

        private void GenerateBlocksForSession(Session session)
        {
            for(int i = 1; i<(int)session.PlanSize*(int)session.PlanSize; i++)
            {
                session.Blocks.Add(new BlockRoom()
                {
                    BlockPosition = i,
                });
            }
            _uow.Save();
        }
    }
}
