using BoardGame.Domain.Entities.EntityEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BoardGame.Domain.Models
{
    public class SessionModel
    {
        public int SessionId { get; set; }
        public string SessionName { get; set; }
        public string SessionPassword { get; set; }
        public SessionType SessionType { get; set; }
        public PlanSize PlanSize { get; set; }
        public int CenterBlockPosition { get; set; }

        public ICollection<BlockModel> Blocks { get; set; }

        public List<List<BlockModel>> ConstructTwoDimensionalBoard()
        {
            var board = new List<List<BlockModel>>();

            var index = -1;
            foreach (var element in this.Blocks)
            {
                if (element.BlockPositionY != index)
                {
                    board.Add(new List<BlockModel>());
                    index = element.BlockPositionY;
                }
                board.Last().Add(element);
            }
            return board;
        }
    }
}
