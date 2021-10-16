using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;

        public SessionService(ILogger<SessionService> logger,
            IAppUserService appUserService,
            ISessionRepository sessionRepository,
            IMapper mapper,
            IBlockTypeRepository blockTypeRepository,
            IAppUserRepository appUserRepository)
        {
            _blockTypeRepository = blockTypeRepository;
            _logger = logger;
            _appUserService = appUserService;
            _sessionRepository = sessionRepository;
            _appUserRepository = appUserRepository;
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

        public async Task<ActivePlayerModel> ChangeActivePlayer(int sessionId)
        {
            var session = await _sessionRepository.Get(sessionId);
            var playerList = (await _appUserRepository.GetAll(u => u.SessionId == sessionId, x => x.OrderBy(q => q.JoinedSessionAt))).ToList();
            if (playerList == null || !playerList.Any())
            {
                //vyfuc do prazdna
                return null;
            }
            var currentPlayer = playerList.FirstOrDefault(p => p.Id == session.CurrentPlayerId);
            var activePlayerModel = new ActivePlayerModel();
            var sessionMove = session.LastTurnChange ?? default(DateTime);
            activePlayerModel.RemainingSeconds = 59-(int)(DateTime.UtcNow - sessionMove).TotalSeconds;
            session.LastTurnChange = DateTime.UtcNow;
            if(currentPlayer != null)
            {
                var indexOfCurrentPlayer = playerList.IndexOf(currentPlayer);
                var nextPlayer = playerList.ElementAt(++indexOfCurrentPlayer % playerList.Count);
                session.CurrentPlayerId = nextPlayer.Id;
                _sessionRepository.Save();
                activePlayerModel.PlayerName = nextPlayer.UserName;
                activePlayerModel.RemainingSeconds = 59;
                return activePlayerModel;
            }
            else
            {
                var nextPlayer = playerList.ElementAt(0);
                session.CurrentPlayerId = nextPlayer.Id;
                _sessionRepository.Save();
                activePlayerModel.PlayerName = nextPlayer.UserName;
                activePlayerModel.RemainingSeconds = 59;
                return activePlayerModel;
            }
        }

        public async Task<ActivePlayerModel> GetActivePlayer(int sessionId)
        {
            var session = await _sessionRepository.Get(sessionId);
            if(session.CurrentPlayerId == null)
            {
                return null;
            }
            var player = await _appUserRepository.Get(session.CurrentPlayerId.Value);
            var activePlayerModel = new ActivePlayerModel();
            activePlayerModel.PlayerName = player.UserName;
            var sessionMove = session.LastTurnChange ?? default(DateTime);
            activePlayerModel.RemainingSeconds = 57-(int)(DateTime.UtcNow - sessionMove).TotalSeconds;

            return activePlayerModel;
        }
    }
}
