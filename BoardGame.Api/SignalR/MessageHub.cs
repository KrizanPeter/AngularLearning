using AutoMapper;
using BoardGame.Api.DTOs.MessageDto;
using BoardGame.Domain.Models;
using BoardGame.Services.Services.AuthServices;
using BoardGame.Services.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.Api.SignalR
{
    public class MessageHub : Hub
    {
        private readonly IChatMessageService _chatMessageService;
        private readonly IMapper _mapper;

        public MessageHub(IChatMessageService chatMessageService, IMapper mapper)
        {
            _chatMessageService = chatMessageService;
            _mapper = mapper;
        }

        public async Task SendMessage(MessageDto message)
        {
            var messageModel = _mapper.Map<ChatMessageModel>(message);

            var userName = Context.User.GetUserName();
            _ = await _chatMessageService.AddNewMessage(messageModel, userName);
            var result = await _chatMessageService.GetMessagesForSession(userName);

            if(result.Succeeded)
            {
                await Clients.All.SendAsync("RecieveInstantMessage", result.Data);
            }
        }
    }
}
