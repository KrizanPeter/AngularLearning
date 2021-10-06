namespace BoardGame.Domain.Models
{
    public class MovementModel
    {
        public bool ExitTop { get; set; }
        public bool ExitDown { get; set; }
        public bool ExitRight { get; set; }
        public bool ExitLeft { get; set; }
        public bool InvalidMovement {
            get {
                return !ExitDown && !ExitLeft && !ExitRight && !ExitTop;
            }
        }
    }
}
