
using BoardGame.Api.SignalR;
using BoardGame.Services.Services.Interfaces;

using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace BoardGame.Api.BackgroundServices
{
    public class TurnBackgroundServiceAction : TurnBackgroundService
    {
        private readonly IHubContext<BoardHub> _hubContext;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public TurnBackgroundServiceAction(IServiceScopeFactory serviceScopeFactory, IHubContext<BoardHub> hubContext)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _hubContext = hubContext;
        }
        protected override async void EndCurrentTurnAsync(object state)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var sessionService = scope.ServiceProvider.GetRequiredService<ISessionService>();
                var heroService = scope.ServiceProvider.GetRequiredService<IHeroService>();

                await heroService.HealAllHeroesEOR();
                var sessions = await sessionService.GetSessions();
                foreach (var session in sessions.Data)
                {                 
                    var nextUser = sessionService.ChangeActivePlayer(session.SessionId);
                    var gameGroup = "game-session-" + session.SessionId;
                    await _hubContext.Clients.Group(gameGroup).SendAsync("EndTurnDetected", nextUser.Result);
                }
            }
        }
    }
}
