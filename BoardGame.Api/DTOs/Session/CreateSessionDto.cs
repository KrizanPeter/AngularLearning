using BoardGame.Domain.Entities.EntityEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs.Session
{
    public class CreateSessionDto
    {
        public string SessionName { get; set; }
        public string SessionPassword { get; set; }
        public PlanSize PlanSize { get; set; } = PlanSize.Small;
    }
}
