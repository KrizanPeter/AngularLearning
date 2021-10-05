using System;
using System.Threading.Tasks;

using AutoMapper;

using BoardGame.Api.DTOs.MessageDto;
using BoardGame.Domain.Models;
using BoardGame.Services.Services.AuthServices;
using BoardGame.Services.Services.Interfaces;

using Microsoft.AspNetCore.SignalR;

namespace BoardGame.Api.SignalR
{
    public class MessageHub : Hub
    {
        private readonly IChatMessageService _chatMessageService;
        private readonly IMapper _mapper;
        private readonly IAppUserService _appUserService;


        public MessageHub(IChatMessageService chatMessageService, IMapper mapper, IAppUserService appUserService)
        {
            _chatMessageService = chatMessageService;
            _mapper = mapper;
            _appUserService = appUserService;
        }

        public override async Task OnConnectedAsync()
        {
            var userName = Context.User.GetUserName();

            var user = await _appUserService.GetAppUser(userName);
            var session = "chat-session-" + (user.SessionId ?? default(int));

            await Groups.AddToGroupAsync(Context.ConnectionId, session);


            var result = await _chatMessageService.GetMessagesForSession(userName);
            await Clients.Caller.SendAsync("RecieveInstantMessage", result.Data);
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            var session = "chat-session-" + Context.GetHttpContext().Request.Query["sessionId"];
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, session);
        }


        public async Task SendMessage(MessageDto message)
        {
            var messageModel = _mapper.Map<ChatMessageModel>(message);

            var userName = Context.User.GetUserName();
            var user = await _appUserService.GetAppUser(userName);
            var session = "chat-session-" + user.SessionId;
            messageModel.SessionId = user.SessionId ?? default(int);

            _ = await _chatMessageService.AddNewMessage(messageModel, userName);
            var result = await _chatMessageService.GetMessagesForSession(userName);

            if(result.Succeeded)
            {
                await Clients.Group(session).SendAsync("RecieveInstantMessage", result.Data);
            }
        }
    }
}
