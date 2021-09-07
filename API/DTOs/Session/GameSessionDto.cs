using BoardGame.Domain.Entities.EntityEnums;

namespace API.DTOs.Session
{
    public class GameSessionDto
    {
        public int GameSessionId { get; set; }
        public int GamePlanId { get; set; }
        public string SessionName { get; set; }
        public string SessionPassword { get; set; }
        public SessionType SessionType { get; set; }
    }
}
