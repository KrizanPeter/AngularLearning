using AutoMapper;
using BoardGame.Domain.Entities;
using BoardGame.Domain.Models;
using BoardGame.Domain.Repositories.Interfaces;
using BoardGame.Services.ReturnStates;
using BoardGame.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame.Services.Services
{
    public class ChatMessageService : IChatMessageService
    {
        public readonly IChatMessageRepository _chatMessageRepository;
        public readonly IAppUserService _appUserService;
        public readonly IMapper _mapper;

        public ChatMessageService(IChatMessageRepository chatMessageRepository, IAppUserService appUserService, IMapper mapper)
        {
            _chatMessageRepository = chatMessageRepository;
            _appUserService = appUserService;
            _mapper = mapper;
        }

        public async Task<OperationalResult> AddNewMessage(ChatMessageModel message, string userName)
        {
            var user = await _appUserService.GetAppUser(userName);

            var messageEntity = new ChatMessage()
            {
                SessionId = user.SessionId ?? default(int),
                Message = message.Message,
                Sender = userName,
                AppUserId = user.Id
            };

            _chatMessageRepository.Add(messageEntity);
            _chatMessageRepository.Save();

            return OperationalResult.Success();
        }

        public async Task<OperationalResult<List<ChatMessageModel>>> GetMessagesForSession(string userName)
        {
            var user = await _appUserService.GetAppUser(userName);
            var result = _chatMessageRepository.GetLatestMessages(user.SessionId ?? default(int));
            var messageModels = _mapper.Map<List<ChatMessageModel>>(result);
            return OperationalResult.Success(messageModels);
        }
    }
}
