
using Microsoft.AspNetCore.Authorization;

namespace BoardGame.Api.Authorizations.TurnAuthorization
{
    public class IsOnTurnRequirement : IAuthorizationRequirement
    {
    }
}
