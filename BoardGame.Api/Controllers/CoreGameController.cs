using AutoMapper;
using BoardGame.Api.DTOs.Block;
using BoardGame.Api.DTOs.CoreGame;
using BoardGame.Api.DTOs.Session;
using BoardGame.Domain.Entities.EntityEnums;
using BoardGame.Domain.Repositories.Interfaces;
using BoardGame.Services.Services.AuthServices;
using BoardGame.Services.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardGame.Api.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class CoreGameController : ControllerBase
    {
        private readonly ILogger<CoreGameController> _logger;
        public IMapper _mapper { get; }
        private readonly IAppUserService _appUserService;
        private readonly ISessionService _sessionService;

        public CoreGameController(ILogger<CoreGameController> logger, IMapper mapper, IAppUserService appUserService, ISessionService sessionService)
        {
            _logger = logger;
            _mapper = mapper;
            _appUserService = appUserService;
            _sessionService = sessionService;
        }

        [Authorize]
        [HttpPost("loadgame")]
        public async Task<ActionResult> LoadSessionAsync(DeckWindowDto deckWindowDto)
        {
            var user = await _appUserService.GetAppUser(User.GetUserName());

            if(user.SessionId == null || user.SessionId == 0)
            {
                return BadRequest("User has no active session");
            }

            var result = await _sessionService.LoadSessionAsync(user.SessionId?? default(int), deckWindowDto.startX, deckWindowDto.startY, deckWindowDto.endX, deckWindowDto.endY);

            var dto = _mapper.Map<GameSessionDto>(result.Data);
            dto.BlocksShape = _mapper.Map<ICollection<ICollection<GameBlockDto>>>(result.Data.ConstructTwoDimensionalBoard());
            dto.Blocks = null;
            return Ok(dto);
        }

        [Authorize]
        [HttpPost("pickhero")]
        public async Task<ActionResult> PickHeroAsync()
        {

        }
    }
}
