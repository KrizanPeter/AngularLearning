using System.Threading.Tasks;

using BoardGame.Domain.Repositories.Interfaces;
using BoardGame.Services.Services.AuthServices;
using BoardGame.Services.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace BoardGame.Api.Authorizations.TurnAuthorization
{
    public class IsOnTurnHandler : AuthorizationHandler<IsOnTurnRequirement>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public IsOnTurnHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;

        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IsOnTurnRequirement requirement)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var sessionRepository = scope.ServiceProvider.GetRequiredService<ISessionRepository>();
                var appUserService = scope.ServiceProvider.GetRequiredService<IAppUserService>();

                var userName = context.User.GetUserName();
                var user = appUserService.GetAppUser(userName).Result;
                //context.Fail();
                if (user != null && user.SessionId != null)
                {
                    var session = sessionRepository.Get(user.SessionId.Value).Result;
                    if (session.CurrentPlayerId == user.Id)
                    {
                        context.Succeed(requirement);
                    }
                }
            }
            return Task.CompletedTask;

        }
    }
}
