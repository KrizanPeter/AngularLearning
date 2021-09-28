using API.Entities.Context;
using AutoMapper;
using BoardGame.Domain.Entities;
using BoardGame.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Domain.Repositories
{
    public class ChatMessageRepository : Repository<ChatMessage>, IChatMessageRepository
    {
        private readonly DataContext _db;
        private readonly IMapper _mapper;

        public ChatMessageRepository(DataContext db, IMapper mapper) : base(db)
        {
            _db = db;
            _mapper = mapper;
        }

        public List<ChatMessage> GetLatestMessages(int sessionId)
        {
            var result = _db.ChatMessages
                .Where(a => a.SessionId == sessionId)
                .ToList()
                .TakeLast(10);

            return result.ToList();
        }
    }
}
