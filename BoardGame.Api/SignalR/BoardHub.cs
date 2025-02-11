﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using BoardGame.Api.DTOs.Block;
using BoardGame.Domain.Models;
using BoardGame.Services.Services.AuthServices;
using BoardGame.Services.Services.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace BoardGame.Api.SignalR
{
    public class BoardHub : Hub
    {
        private readonly IMapper _mapper;
        private readonly IHeroService _heroService;
        private readonly IBlockService _blockService;
        private readonly IAppUserService _appUserService;

        public BoardHub(IMapper mapper, IHeroService heroService, IBlockService blockService, IAppUserService userService)
        {
            _mapper = mapper;
            _heroService = heroService;
            _blockService = blockService;
            _appUserService = userService;
        }

        public override async Task OnConnectedAsync()
        {
            var session = "game-session-" + Context.GetHttpContext().Request.Query["sessionId"];

            await Groups.AddToGroupAsync(Context.ConnectionId, session);
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            var session = "game-session-" + Context.GetHttpContext().Request.Query["sessionId"];
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, session);
        }

        [Authorize(Policy = "IsOnTurn")]
        public async Task MoveHero(GameBlockDto targetBlock)
        {
            var userName = Context.User.GetUserName();
            var user = await _appUserService.GetAppUser(userName);
            var hero = await _heroService.GetHeroInformationOfUser(user.Id);
            var comingFromBlockId = hero.Data.BlockId;
            var gameGroup = "game-session-" + user.SessionId;

            var result = await _blockService.MoveHeroToBlock(user.Id, targetBlock.BlockId);

            if (result.Succeeded)
            {
                await Clients.Group(gameGroup).SendAsync("MovementDetected", result.Data);
            }
            else
            {
                await Clients.Caller.SendAsync("MovementFailed", result.Errors);
                return;
            }

            if (!IsNecessaryResolveBattle(result.Data, user.Id)) { return; }

            var report = await _blockService.ResolveConflictOnBlock(targetBlock.BlockId, hero.Data.AppUserId);

            if (report.Succeeded)
            {
                await Clients.Group(gameGroup).SendAsync("BattleReport", new List<BattleReportModel>() { report.Data });
            }
            else
            {
                await Clients.Caller.SendAsync("MovementFailed", result.Errors);
                return;
            }

            if (report.Data.AttackerHealthCurrent <= 0 || report.Data.DefenderHealthCurrent > 0)
            {
                var res = await _blockService.MoveHeroToBlock(user.Id, comingFromBlockId);

                if (res.Succeeded)
                {
                    await Clients.Group(gameGroup).SendAsync("MovementDetected", res.Data);
                }
                else
                {
                    await Clients.Caller.SendAsync("MovementFailed", result.Errors);
                    return;
                }
            }
        }

        private bool IsNecessaryResolveBattle(List<BlockModel> data, int id)
        {
            var blocks = data.Where(a => a.Heroes.Any(a => a.AppUserId == id));

            foreach (var block in blocks)
            {
                if (block.Heroes.Count >= 2) return true;
                else if (block.Heroes.Any() && block.Monster != null) return true;
            }
            return false;
        }


    }
}
