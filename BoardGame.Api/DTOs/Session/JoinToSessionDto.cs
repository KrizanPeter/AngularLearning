using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Session
{
    public class JoinToSessionDto
    {
        public int SessionId { get; set; }
        public string UserName { get; set; }
    }
}
