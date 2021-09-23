using AutoMapper;
using BoardGame.Domain.Entities;
using BoardGame.Domain.Models;
using BoardGame.Domain.Repositories.Interfaces;
using BoardGame.Services.ReturnStates;
using BoardGame.Services.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Services.Services
{
    public class SessionService : ISessionService
    {
        private readonly ILogger<SessionService> _logger;
        private readonly IAppUserService _appUserService;
        private readonly ISessionRepository _sessionRepository;
        private readonly IMapper _mapper;

        public SessionService(ILogger<SessionService> logger, IAppUserService appUserService, ISessionRepository sessionRepository, IMapper mapper)
        {
            _logger = logger;
            _appUserService = appUserService;
            _sessionRepository = sessionRepository;
            _mapper = mapper;
        }

        public Task<OperationalResult> AddSession(int userId, SessionModel sessionModel)
        {
            if(sessionModel == null)
            {
                return Task.FromResult(OperationalResult.Failed(new OperationalError(HttpStatusCode.InternalServerError, "Invalid sessionModel")));
            }

            var session = _mapper.Map<Session>(sessionModel);
            session = GenerateBlocksForSession(session);
            _sessionRepository.Add(session);
            _sessionRepository.Save();
            
            var result = _appUserService.AddSessionToUserAsync(userId, session.SessionId);

            return result;
        }

        public async Task<OperationalResult<SessionModel>> GetSessionById(int id)
        {
            var session = await _sessionRepository.GetFirstOrDefault(a => a.SessionId == id);
            var sessionModel = _mapper.Map<SessionModel>(session);
            return new OperationalResult<SessionModel>(sessionModel);
        }

        public async Task<OperationalResult<IEnumerable<SessionModel>>> GetSessions()
        {
            var sessions = await _sessionRepository.GetAll();
            var sessionsModel = _mapper.Map<IEnumerable<SessionModel>>(sessions);
            return new OperationalResult<IEnumerable<SessionModel>>(sessionsModel);
        }

        public Task<OperationalResult<SessionModel>> LoadSessionAsync(int sessionId, int startX, int startY, int endX, int endY)
        {
            var session = _sessionRepository.GetSessionWithBlocks(sessionId, startX, startY, endX, endY);
            if (session == null)
            {
                return Task.FromResult(OperationalResult.Failed<SessionModel>(new OperationalError(HttpStatusCode.NotFound, "Session not found")));
            }

            return Task.FromResult(OperationalResult.Success(session));
        }

        private Session GenerateBlocksForSession(Session session)
        {

            session.Blocks = new List<Block>();
            int blockOrder = 1;
            for (int i = 1; i <= (int)session.PlanSize; i++)
            {
                for (int j = 1; j <= (int)session.PlanSize; j++)
                    session.Blocks.Add(new Block()
                    {
                        SessionId = session.SessionId,
                        BlockType = blockOrder == ((int)session.PlanSize * (int)session.PlanSize)/2+1 ? Domain.Entities.EntityEnums.BlockType.Room : Domain.Entities.EntityEnums.BlockType.Hidden,
                        BlockPositionX = j,
                        BlockPositionY = i,
                        BlockOrder = blockOrder++
                    });
                ;
            }
            return session;
        }
    }
}
