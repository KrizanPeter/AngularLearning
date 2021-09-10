using BoardGame.Domain.Entities.EntityEnums;

namespace API.DTOs.Session
{
    public class SessionDto
    {
        public int SessionId { get; set; }
        public string SessionName { get; set; }
        public string SessionPassword { get; set; }
        public SessionType SessionType { get; set; }
    }
}
