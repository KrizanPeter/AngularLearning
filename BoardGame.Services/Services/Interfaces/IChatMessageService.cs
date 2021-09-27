using BoardGame.Domain.Models;
using BoardGame.Services.ReturnStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Services.Services.Interfaces
{
    public interface IChatMessageService
    {
        Task<OperationalResult> AddNewMessage(ChatMessageModel message, string UserName);
        Task<OperationalResult<List<ChatMessageModel>>> GetMessagesForSession(string UserName);
    }
}
