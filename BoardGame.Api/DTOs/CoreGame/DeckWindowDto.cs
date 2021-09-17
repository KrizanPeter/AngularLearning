using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.Api.DTOs.CoreGame
{
    public class DeckWindowDto
    {
        public int  startX {get; set;} 
        public int  startY {get; set;} 
        public int  endX {get; set;} 
        public int  endY {get; set; }
    }
}
