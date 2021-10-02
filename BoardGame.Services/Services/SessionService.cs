using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

using AutoMapper;

using BoardGame.Domain.Entities;
using BoardGame.Domain.Models;
using BoardGame.Domain.Repositories.Interfaces;
using BoardGame.Services.ReturnStates;
using BoardGame.Services.Services.Interfaces;

using Microsoft.Extensions.Logging;

namespace BoardGame.Services.Services
{
    public class SessionService : ISessionService
    {
        private readonly ILogger<SessionService> _logger;
        private readonly IAppUserService _appUserService;
        private readonly ISessionRepository _sessionRepository;
        private readonly IBlockTypeRepository _blockTypeRepository;
        private readonly IMapper _mapper;

        public SessionService(ILogger<SessionService> logger, IAppUserService appUserService, ISessionRepository sessionRepository, IMapper mapper, IBlockTypeRepository blockTypeRepository)
        {
            _blockTypeRepository = blockTypeRepository;
            _logger = logger;
            _appUserService = appUserService;
            _sessionRepository = sessionRepository;
            _mapper = mapper;
        }

        public async Task<OperationalResult> AddSession(int userId, SessionModel sessionModel)
        {
            if(sessionModel == null)
            {
                return OperationalResult.Failed(new OperationalError(HttpStatusCode.InternalServerError, "Invalid sessionModel"));
            }

            var session = _mapper.Map<Session>(sessionModel);
            session = await GenerateBlocksForSessionAsync(session);
            _sessionRepository.Add(session);
            _sessionRepository.Save();
            
            return OperationalResult.Success();
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

        private async Task<Session> GenerateBlocksForSessionAsync(Session session)
        {

            var blockTypeHidden = await _blockTypeRepository.GetFirstOrDefault(a => a.BlockCategory == Domain.Entities.EntityEnums.BlockCategory.Hidden);
            var blockTypeMiddle = await _blockTypeRepository.GetFirstOrDefault(a => a.BlockCategory == Domain.Entities.EntityEnums.BlockCategory.Room && a.ExitDown && a.ExitLeft && a.ExitRight && a.ExitTop);

            session.Blocks = new List<Block>();
            int blockOrder = 1;
            for (int i = 1; i <= (int)session.PlanSize; i++)
            {
                for (int j = 1; j <= (int)session.PlanSize; j++)
                {
                    var middleBlock = blockOrder == ((int)session.PlanSize * (int)session.PlanSize) / 2 + 1;
                    session.Blocks.Add(new Block()
                    {
                        SessionId = session.SessionId,
                        BlockType = middleBlock ? blockTypeMiddle : blockTypeHidden,
                        BlockPositionX = j,
                        BlockPositionY = i,
                        BlockOrder = blockOrder++,
                    });
                }
            }
            return session;
        }
    }
}
