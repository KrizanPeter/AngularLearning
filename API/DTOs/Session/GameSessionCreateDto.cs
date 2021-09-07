using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Session
{
    public class GameSessionCreateDto
    {
        public string SessionName { get; set; }
        public string SessionPassword { get; set; }
    }
}
