using BoardGame.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Services.Services.Interfaces
{
    public interface ISessionService
    {
        Session LoadOrFillSessionAsync(int sessionId);
    }
}
