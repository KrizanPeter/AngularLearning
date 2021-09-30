using System.Collections.Generic;

using BoardGame.Domain.Entities.EntityEnums;
using BoardGame.Domain.Models.Enums;

namespace BoardGame.Domain.Models
{
    public class MovementModel
    {
        public List<BlockDirection> PossibleDirections { get; set; }
        public bool InvalidMovement { get; }

        public MovementModel()
        {
            InvalidMovement = true;
        }

        public MovementModel(MovementSource comingFrom)
        {
            InvalidMovement = false;
            PossibleDirections = new List<BlockDirection>();
            PossibleDirections.Add(BlockDirection.Cross);
            switch (comingFrom)
            {
                case MovementSource.Down:
                    PossibleDirections.Add(BlockDirection.DownLeft);
                    PossibleDirections.Add(BlockDirection.DownRight);
                    PossibleDirections.Add(BlockDirection.Vertical);
                    break;
                case MovementSource.Left:
                    PossibleDirections.Add(BlockDirection.DownLeft);
                    PossibleDirections.Add(BlockDirection.Horizontal);
                    PossibleDirections.Add(BlockDirection.TopLeft);
                    break;
                case MovementSource.Right:
                    PossibleDirections.Add(BlockDirection.DownRight);
                    PossibleDirections.Add(BlockDirection.Horizontal);
                    PossibleDirections.Add(BlockDirection.TopRight);
                    break;
                case MovementSource.Top:
                    PossibleDirections.Add(BlockDirection.TopLeft);
                    PossibleDirections.Add(BlockDirection.TopRight);
                    PossibleDirections.Add(BlockDirection.Vertical);
                    break;
            }
        }
    }
}
